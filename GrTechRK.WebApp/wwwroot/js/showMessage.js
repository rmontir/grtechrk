var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 5000
});

let swalSuccess = (text) => Toast.fire({
    icon: 'success',
    html: text
});

let swalError = (text) => Toast.fire({
    icon: 'error',
    html: text
});

let toastSuccess = (text) => $(document).Toasts('create', {
    class: 'bg-success',
    title: 'Success',
    autohide: true,
    delay: 5000,
    body: text
})

let toastError = (text) => $(document).Toasts('create', {
    class: 'bg-danger',
    title: 'Error',
    autohide: true,
    delay: 5000,
    body: text
})

let swalConfirmAsync = async (text) => {
    let response = Swal.fire({
        title: 'Are you sure?',
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes'
    });

    return response;
};

let generateErrorMessageWithoutNumber = (errorList) => {
    let error = "";
    errorList.forEach((item, idx) => {
        if (idx > 0) error += "<br />";
        error += `<span>${item}</span>`;
    });

    return error;
};