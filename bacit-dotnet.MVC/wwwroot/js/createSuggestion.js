
$(() => {

    // * HTML-Elements

    // # = id, . = css, [data-x-y="x"], attribute

    const imgInput = $("#imageInput");
    const imgWarning = $("#imageWarning");
    const imgPreview = $("#imagePreview");
    const imgTypeWarning = $("#imageTypeWarning");
    const supportedImageTypes = ["jpeg", "jpg", "png"];


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
                submitBtn.disabled = true;
                return false;
            }
            else {
                imgWarning.hide();
                submitBtn.disabled = false;
                return true;
            }
        }
        return true;
    }

    // Validates the file type the user is trying to upload.
    function validateFileType() {

        var imageInput = document.getElementById('imageInput');
        var fileType = imageInput.files[0].type;
        var extension = fileType.slice((Math.max(0, fileType.lastIndexOf("/")) || Infinity) + 1);

            //Anything file extension that is not in the array is indexOf(-1).
            if (supportedImageTypes.indexOf(extension) > -1) {
                imgTypeWarning.hide();
                submitBtn.disabled = false;
                return true;
            }
            else {
                imgTypeWarning.text("Filtypen du lastet opp er ikke støttet. Vennligst velg en de følgenede filtypene: "+supportedImageTypes+".");
                imgTypeWarning.show();
                submitBtn.disabled = true;
                return false;
            }

        return true;
    }
});