(function (root, undefined) {
    $(document).ready(main);
    const cache = new Map();

    function main() {
        initSelectList();
        initCollapsible();
    }

    function initCollapsible() {
        toggleMoreInfo.toggled = false;

        $('#moreInfo').hide();
        $('#btnMoreInfo').click(toggleMoreInfo);
    }
    function toggleMoreInfo() {
        this.toggled = !this.toggled;

        let packageId = $('#selectedPackage').val();

        if (this.toggled) {
            $('#btnMoreInfo > span').css({ 'padding-right': '0.15em','line-height': '0.5','writing-mode': 'vertical-rl', 'text-orientation': 'mixed' });
            if (!cache.has(packageId)) {
                $.getJSON(`/api/package/products/${packageId}`, { format: 'json' })
                    .done((data) => {
                        cache.set(packageId, data);
                        displayProductsSuppliersData(data);
                    })
                    .fail(() => {
                        $('#moreInfo').empty();
                        $('#moreInfo').append(`No information to show`);
                        $('#moreInfo').show();
                    });
            } else {
                displayProductsSuppliersData(cache.get(packageId));
            }
        } else {
            $('#btnMoreInfo > span').css({ 'padding-right': '', 'line-height': '','writing-mode': 'horizontal-tb', 'text-orientation': '' });
            $('#moreInfo').hide();
            $('#moreInfo').empty();
        }
    }

    function displayProductsSuppliersData(data) {
        $('#moreInfo').show();
        $('#moreInfo').empty();
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
    }

    function initSelectList() {
        document.querySelector('#selectedPackage').addEventListener('change', (e) => {
            if (e.target.value == 0) {
                $('#pkgInfo').css('display', 'none');
            } else {
                $('#pkgInfo').show();
                getSelectedPackage(e.target.value);
            }
        });
    }

    function getSelectedPackage(pkgId) {
        $('#moreInfo').empty();
        $('#moreInfo').hide();
        // Request the package information
        $.getJSON(`/api/package/${pkgId}`, { format: 'json' })
            .done(displayPkgInfo);

        // Request the package gallery image paths for this package
        $.getJSON(`/api/package/gallery/${pkgId}`, { format: 'json' })
            .done(displayImages);
    }

    function displayPkgInfo(data) {
        console.log(data);
        if ('errorMessage' in data) {
            $('#pkgInfo').prepend('<h3 id="errorMsg">Unable to retrieve package info.</h3>');
            $('#pkgInfoContent').hide();
            return;
        } else {
            $('#errorMsg').remove();
        }
        $('#pkgInfoContent').show();
        $('#selectedPackageId').val(data['packageId']);
        for (let key in data) {
            var output = data[key];
            if (key === 'pkgStartDate' || key === 'pkgEndDate') {
                output = new Date(data[key]).toLocaleDateString("en-US");
            }
            if (key === 'pkgBasePrice') {
                var f = Intl.NumberFormat('en-US',
                    {
                        style: "currency",
                        currency: "USD",
                    });
                output = f.format(data[key]);
            }
            try {
                $(`#${key}`).html(output);
            } catch { }
        }
    }

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