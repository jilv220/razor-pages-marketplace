// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let toastElList;
let toastList;

const sleepP = new Promise((resolve) => {
    setTimeout(() => {
        resolve()
    }, 1000)
})

window.addEventListener('load', async () => {
    await sleepP.then(() => {
        toastElList = document.querySelectorAll('.toast')
        toastList = [...toastElList].map(toastEl => new bootstrap.Toast(toastEl, {
            delay: 1500
        }))
        console.log('Toasts Initialized...')
        console.log(toastElList)
    })
})

function showAddToCartToast() {
    const toast = document.getElementById('addToCartToast')
    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toast)
    toastBootstrap.show()
}