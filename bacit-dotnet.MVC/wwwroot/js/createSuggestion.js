
$(() => {

    // * HTML-Elements

    // # = id, . = css, [data-x-y="x"], attribute

    const imgInput = $("#imageInput");
    const imgInput2 = $("#imageInput2");
    const imgWarning = $("#imageWarning");
    const imgTypeWarning = $("#imageTypeWarning");
    const imgWarning2 = $("#imageWarning2");
    const imgTypeWarning2 = $("#imageTypeWarning2");
    const imgPreview = $("#imagePreview");
    const supportedImageTypes = ["jpeg", "jpg", "png","heic","heif"];

    //One unique variable for each validation function
    var unvalidSize1 = 0;
    var unvalidSize2 = 0;
    var unvalidExtension1 = 0;
    var unvalidExtension2 = 0;

    let fileReader = new FileReader();

    // * Listen for file change on imageInput
    imgInput.on("change", () => {

        validateFileInput();
        validateFileType();
        checkImageInputs();

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

    imgInput2.on("change", () => {

        validateFileInput2();
        validateFileType2();
        checkImageInputs();

    });

    // * Listen for form submittion
    $("form").submit((e) => {
        const isValid = validateFileInput();

        if (!isValid) e.preventDefault();

        return isValid;
    });


    //Validates the file size the user is trying to upload.
    function validateFileInput() {
        if (imgInput.get(0).files.length > 0) {
            const fileSize = Math.round(Number(imgInput.get(0).files[0].size) / (1024.0 * 1024.0)); // in MB
            const maxFileSize = Number(imgInput.data("maxSize")) / (1024.0 * 1024.0); // in MB;

            if (fileSize > maxFileSize) {
                imgWarning.text("Fil størrelse på: " + fileSize.toString() + " MB er større enn maks tillatt " + maxFileSize.toString() + " MB");
                imgWarning.show();
                submitBtn.disabled = true;
                unvalidSize1 = 1;
                return false;
            }
        }
        imgWarning.hide();
        unvalidSize1 = 0;
        return true;
    }

    //Validates the file type the user is trying to upload.
    function validateFileType() {
        if (imgInput.get(0).files.length > 0) {
            var imageInput = document.getElementById('imageInput');
            var fileType = imageInput.files[0].type;
            var extension = fileType.slice((Math.max(0, fileType.lastIndexOf("/")) || Infinity) + 1);

            //Any file extension that is not in the array is indexOf(-1).
            if (supportedImageTypes.indexOf(extension) < 0) {
                imgTypeWarning.text("Innvalid filtype. Vennligst velg en de følgenede filtypene: " + supportedImageTypes + ".");
                imgTypeWarning.show();
                submitBtn.disabled = true;
                unvalidExtension1 = 1;
                return false;
                alert(extension);
            }
        }
        imgTypeWarning.hide();
        unvalidExtension1 = 0;
        return true;   
    }



    //Validates the file size the user is trying to upload.
    function validateFileInput2() {
        if (imgInput2.get(0).files.length > 0) {
            const fileSize2 = Math.round(Number(imgInput2.get(0).files[0].size) / (1024.0 * 1024.0)); // in MB
            const maxFileSize2 = Number(imgInput2.data("maxSize")) / (1024.0 * 1024.0); // in MB;

            if (fileSize2 > maxFileSize2) {
                imgWarning2.text("Fil størrelse på: " + fileSize2.toString() + " MB er større enn maks tillatt " + maxFileSize2.toString() + " MB");
                imgWarning2.show();
                submitBtn.disabled = true;
                unvalidSize2 = 1;
                return false;
            }
        }
        imgWarning2.hide();
        unvalidSize2 = 0;
        return true;
    }

    //Validates the file type the user is trying to upload.
    function validateFileType2() {
        if (imgInput2.get(0).files.length > 0) {
            var imageInput2 = document.getElementById('imageInput2');
            var fileType2 = imageInput2.files[0].type;
            var extension2 = fileType2.slice((Math.max(0, fileType2.lastIndexOf("/")) || Infinity) + 1);

            //Any file extension that is not in the array is indexOf(-1).
            if (supportedImageTypes.indexOf(extension2) < 0) {
                imgTypeWarning2.text("Filtypen du lastet opp er ikke støttet. Vennligst velg en de følgenede filtypene: " + supportedImageTypes + ".");
                imgTypeWarning2.show();
                submitBtn.disabled = true;
                unvalidExtension2 = 1;
                return false;
            }
        }
        imgTypeWarning2.hide();
        unvalidExtension2 = 0;
        return true;
    }

    //Checks that all validation functions are accepting the users input before enabling the submit button.
    function checkImageInputs() {
        if (unvalidSize1 == 0 && unvalidExtension1 == 0 && unvalidSize2 == 0 && unvalidExtension2 == 0) {
            submitBtn.disabled = false;
        }
    }

});