$.validator.addMethod('filesize', (value, element, param) => {
    let isValid = this.optional(element) || element.files[0].size <= param;
    return isValid;
})