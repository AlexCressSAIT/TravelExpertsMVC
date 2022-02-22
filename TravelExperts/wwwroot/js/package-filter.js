/* Handles AJAX calls on the package booking page */
(function (root, undefined) {
    // Don't do anything until the DOM is available
    $(document).ready(main);

    // Cache, so we don't have to retrieve the products and suppliers over and over
    const cache = new Map();

    // Runs once the document is ready
    function main() {
        initSelectList();
        initCollapsible();
    }

    // Initializes the collapsible section of product and supplier info
    function initCollapsible() {
        toggleMoreInfo.toggled = false;     // the current state of the collapsible

        // Start off hidden, add click listener
        $('#moreInfo').hide();
        $('#btnMoreInfo').click(toggleMoreInfo);
    }

    // Toggles the more info section being displayed. Downloads and caches information
    function toggleMoreInfo() {
        this.toggled = !this.toggled;   // switch the toggle

        // Get the selected package
        let packageId = $('#selectedPackage').val();

        // If we are displaying the information
        if (this.toggled) {
            // Save the document scroll position so we can go back later
            toggleMoreInfo.scroll = window.scrollY;

            // button styling
            $('#btnMoreInfo > span').css({ 'padding-right': '0.15em','line-height': '0.5','writing-mode': 'vertical-rl', 'text-orientation': 'mixed' });

            // Check the cache for the info, otherwise make an AJAX request
            if (!cache.has(packageId)) {
                $.getJSON(`/api/package/products/${packageId}`, { format: 'json' })
                    .done((data) => {
                        // Cache the data
                        cache.set(packageId, data);

                        // display the data
                        displayProductsSuppliersData(data);
                    })
                    .fail(() => {
                        // If it fails for some reason, just clear everything and show an error message
                        $('#moreInfo').empty();
                        $('#moreInfo').append(`No information to show`);
                        $('#moreInfo').show();
                    });
            } else {
                // Just display the info from the cache
                displayProductsSuppliersData(cache.get(packageId));
            }
        // If we are hiding the information
        } else {
            // Button collapse styling
            $('#btnMoreInfo > span').css({ 'padding-right': '', 'line-height': '', 'writing-mode': 'horizontal-tb', 'text-orientation': '' });

            // hide everything and return to old scroll position
            $('#moreInfo').hide();
            $('#moreInfo').empty();
            window.scrollTo(0, toggleMoreInfo.scroll);
        }
    }

    // Display the product supplier data in a table
    function displayProductsSuppliersData(data) {
        $('#moreInfo').show();
        $('#moreInfo').empty();

        // Add content to the DOM
        $('#moreInfo').append(`
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Includes</th>
                        <th>Supplied by</th>
                    </tr>
                </thead>
                <tbody id="productInfo">

                </tbody>
            </table>
        `);
        if (Array.isArray(data)) {
            data.forEach(item => {
                $('#productInfo').append(`
                    <tr>
                        <td class="col">${item.productName}</td>
                        <td class="col">${item.supplierName}</td>
                    </tr>
                 `)
            });
        }

        // scroll to the bottom of the page
        window.scrollTo(0, document.body.scrollHeight);
    }

    // Populate the select list with vacation packages
    function initSelectList() {
        // load the options
        loadSelectOptions();

        // add listener for selection change
        document.querySelector('#selectedPackage').addEventListener('change', (e) => {
            if (e.target.value == 0) {
                // Hide pkgInfo if no package is selected
                $('#pkgInfo').css('display', 'none');
            } else {
                // Show the package info div and get the data for it
                $('#pkgInfo').show();
                getSelectedPackage(e.target.value);
            }
        });
    }

    // Load the select options using AJAX
    function loadSelectOptions() {
        // Display a loading message while packages are downloading
        $('#selectedPackage').append('<option>Loading...</option>');

        // Download the package names and IDs
        $.getJSON(`/api/package/listoptions`, { format: 'json' })
            .done((data) => {
                // Clear the loading message
                $('#selectedPackage').empty();

                // Loop through retrieved packages and add to the select list
                if (Array.isArray(data)) {
                    data.forEach(item => {
                        $('#selectedPackage').append(`
                            <option value=${item.value}>${item.text}</option>
                        `)
                    });
                }
            });
    }

    // Retrieve information about the selected package
    function getSelectedPackage(pkgId) {
        // Hide and remove product suppliers info
        $('#moreInfo').empty();
        $('#moreInfo').hide();
        $('#btnMoreInfo > span').css({ 'padding-right': '', 'line-height': '', 'writing-mode': 'horizontal-tb', 'text-orientation': '' });
        toggleMoreInfo.toggled = false;

        // Request the package information
        $.getJSON(`/api/package/${pkgId}`, { format: 'json' })
            .done(displayPkgInfo);

        // Request the package gallery image paths for this package
        $.getJSON(`/api/package/gallery/${pkgId}`, { format: 'json' })
            .done(displayImages);
    }

    //Updated by: Alex Cress -Added formatting
    // Display the package information
    function displayPkgInfo(data) {
        // Display an error message if something went wrong
        if ('errorMessage' in data) {
            $('#pkgInfo').prepend('<h3 id="errorMsg">Unable to retrieve package info.</h3>');
            $('#pkgInfoContent').hide();
            return;
        } else {
            // Otherwise get rid of the error message
            $('#errorMsg').remove();
        }

        // Show the package info content area
        $('#pkgInfoContent').show();

        // Save the packageId into a hidden input element to send it on later
        $('#selectedPackageId').val(data['packageId']);

        // Loop through all the data and display it in the HTML tags with the associated tag ID's
        for (let key in data) {
            var output = data[key];

            // Format dates properly
            if (key === 'pkgStartDate' || key === 'pkgEndDate') {
                output = new Date(data[key]).toLocaleDateString("en-US");
            }

            // Format currency
            if (key === 'pkgBasePrice') {
                var f = Intl.NumberFormat('en-US',
                    {
                        style: "currency",
                        currency: "USD",
                    });
                output = f.format(data[key]);
            }

            // Try to put the data into the document. Ignore errors
            try {
                $(`#${key}`).html(output);
            } catch { }
        }
    }

    // Retrieve and display images for the selected packages in a carousel
    function displayImages(data) {
        // Get the inner carousel
        const carousel = $('#carousel > .carousel-inner');

        // reset the container
        carousel.empty();

        // if data is an array
        if (Array.isArray(data)) {
            // Loop through all the image paths and load the images into the DOM
            for (let i = 0; i < data.length; i++) {
                const imgSrc = data[i];
                const img = new Image();
                img.src = imgSrc;
                const carouselItem = $('<div></div>');
                carouselItem.addClass('carousel-item');
                if (i === 0)
                    carouselItem.addClass('active');

                carouselItem.append(img);
                carousel.append(carouselItem);
            }
        }
    }
})(window);