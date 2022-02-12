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
        $.getJSON(`/api/package/${pkgId}`, { format: 'json' })
            .done(displayPkgInfo);
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

})(window);