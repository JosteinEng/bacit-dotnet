
$(() => {

    // * HTML-Elements

    // # = id, . = css, [data-x-y="x"], attribute

    const imgInput = $("#imageInput");
    const imgWarning = $("#imageWarning");
    const imgPreview = $("#imagePreview");
    const imgTypeWarning = $("#imageTypeWarning")

    let fileReader = new FileReader();

    // * Listen for file change on imageInput
    imgInput.on("change", () => {

        validateFileInput();
        validateFileType();

        // TODO+Release: Resurrect image-preview

        //fileReader.readAsDataURL(imgInput.get(0).files[0]);
        //fileReader.onload = function () {

        //    imgPreview.onload = function () {
        //        imgPreview.height = 400;
        //        imgPreview.width = 400;
        //    }
        //    imgPreview.onerror = function () {
        //        alert("Make sure image has correct file type");
        //    }
        //    imgPreview.src = fileReader.result.toString();
/*        };*/
    });

    // * Listen for form submittion
    $("form").submit((e) => {
        const isValid = validateFileInput();

        if (!isValid) e.preventDefault();

        return isValid;
    });

    function validateFileInput() {
        if (imgInput.get(0).files.length > 0) {
            const fileSize = Math.round(Number(imgInput.get(0).files[0].size) / (1024.0 * 1024.0)); // in MB
            const maxFileSize = Number(imgInput.data("maxSize")) / (1024.0 * 1024.0); // in MB;

            console.log(imgInput);

            if (fileSize > maxFileSize) {
                imgWarning.text("Fil størrelse på: " + fileSize.toString() + " MB er større enn maks tillatt " + maxFileSize.toString() + " MB");
                imgWarning.show();
                return false;
            }
            else {
                imgWarning.hide();
                return true;
            }
        }
        return true;
    }

    //function validateFileType() {

    //    console.log(imgInput);
        
    //    var fileName = document.getElementById(imgInput).files[0].type;
    //    var extension = fileName.slice((Math.max(0, fileName.lastIndexOf(".")) || Infinity) + 1);
    //    var supportedTypes = ["jpg"];

    //    console.log(supportedTypes);

    //    if (!extension == validFormat) {
    //        imgTypeWarning.text("Test test");
    //        imgTypeWarning.show();
    //        return false;
    //    }
    //    else {
    //        imgTypeWarning.hide();
    //        return true;
    //    }
    //    return true;
    //}
});