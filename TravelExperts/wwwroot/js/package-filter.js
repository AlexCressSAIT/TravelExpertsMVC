(function (root, undefined) {
    $(document).ready(main);

    function main() {
        initSelectList();
    }

    function initSelectList() {
        document.querySelector('#selectedPackage').addEventListener('change', (e) => {
            getSelectedPackage(e.target.value);
        });
    }

    function getSelectedPackage(pkgId) {
        // Request the package information
        $.getJSON(`/api/package/${pkgId}`, { format: 'json' })
            .done(displayPkgInfo);

        // Request the package gallery image paths for this package
        $.getJSON(`/api/package/gallery/${pkgId}`, { format: 'json' })
            .done(displayImages);
    }

    function displayPkgInfo(data) {
        $('#pkgInfo').css('display', 'inherit');
        $('#selectedPackageId').val(data['packageId']);
        for (let key in data) {
            try {
                $(`#${key}`).html(data[key]);
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