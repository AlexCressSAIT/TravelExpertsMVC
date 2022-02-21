/*
 * Script for the cart page. Handles clearing of the cart
 * and warning the user of what is about to happen.
 * Author: Nate Penner
 * February 2022
 */

(function (root, undefined) {
    // Wait until the DOM is loaded before doing anything
    $(document).ready(main);

    // Helper function for setting CSS properties
    // Sets properties on HTMLElement elem, ..args can either be
    // two strings, a property key and value to set it to, or an
    // object containing CSS property key/value pairs
    function setCSS(elem, ...args) {
        if (args.length === 1 && typeof args[0] === 'object') {
            const data = args[0];
            for (let key in data) {
                if (data.hasOwnProperty(key)) {
                    elem.style.setProperty(key, data[key]);
                }
            }
        } else if (args.length === 2 && typeof args[0] === 'string' && typeof args[1] === 'string') {
            elem.style.setProperty(...args);
        }
    }

    // an Overlay (modal) class
    class Overlay {
        constructor(elem) {
            // Hide the element
            setCSS(elem, 'display', 'none');
            this._elem = elem;

            // This covers the page. Set some initial styles
            this._cover = document.createElement('div');
            setCSS(this._cover, {
                'background-color': 'RGBA(25,25,25,0.8)',
                'position': 'fixed',
                'width': '100vw',
                'height': '100vh',
                'top': '0',
                'left': '0',
                'display': 'none'
            });

            // Add the element to the cover
            this._cover.appendChild(elem);

            // Center the element
            setCSS(elem, {
                'position': 'fixed',
                'left': '50%',
                'top': '50%',
                'transform': 'translate(-50%, -50%)'
            });

            // add everything to the DOM
            document.querySelector('body').appendChild(this._cover);
        }

        // Shows the overlay
        show() {
            let body = document.querySelector('body');
            setCSS(body, {
                'overflow': 'hidden'
            });
            setCSS(this._cover, 'display', 'table');
            setCSS(this._elem, 'display', 'inherit');
        }

        // Hides the overlay
        hide() {
            let body = document.querySelector('body');
            body.style.removeProperty('overflow');
            setCSS(this._cover, 'display', 'none');
            setCSS(this._elem, 'display', 'none');
        }
    }

    // Entry point to the script
    function main() {
        // Add click handler to the little X button and some CSS
        $('#clearCartButton').click(handleClearCart).css('cursor', 'pointer');
    }

    // Handles the clicking of the clear cart button
    function handleClearCart() {
        // Create a new overlay using an existing element
        const elem = document.querySelector('#confirmDialog');
        const overlay = new Overlay(elem);

        // Cancel the modal
        $('#confirmNo').click(() => overlay.hide());

        // Confirm the modal (delete the cart items)
        $('#confirmYes').click(() => {
            window.location.href = '/Cart/ClearAll';
            overlay.hide();
        });

        // Show the modal
        overlay.show();
    }
})(window);