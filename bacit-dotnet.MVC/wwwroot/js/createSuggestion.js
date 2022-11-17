
$(() => {

    // * HTML-Elements

    // # = id, . = css, [data-x-y="x"], attribute

    /*All variables and functions which end with "before" belongs to the before image.
    The same goes for the after image.*/

    const imgInputBefore = $("#imageInputBefore");
    const imgInputAfter = $("#imageInputAfter");
    const imgSizeWarningBefore = $("#imageSizeWarningBefore");
    const imgTypeWarningBefore = $("#imageTypeWarningBefore");
    const imgSizeWarningAfter = $("#imageSizeWarningAfter");
    const imgTypeWarningAfter = $("#imageTypeWarningAfter");
    const imgPreview = $("#imagePreview");
    const supportedImageTypes = ["jpeg", "jpg", "png","heic","heif"];

    //One unique variable for each validation function
    var unvalidSizeBefore = 0;
    var unvalidExtensionBefore = 0;
    var unvalidSizeAfter = 0;
    var unvalidExtensionAfter = 0;
    
    // * Listen for file change on imageInputBefore
    imgInputBefore.on("change", (e) => {

        validateFileSizeBefore();
        validateFileTypeBefore();
        checkImageInputs();
        
        if (e.target.files.length === 0) {
            console.log("no files")
            return;
        }
        
        let src = URL.createObjectURL(e.target.files[0]);
               
        imgPreview.attr("src", src);
        imgPreview.css("display", "block");
    });

    // * Listen for file change on imageInputAfter
    imgInputAfter.on("change", () => {

        validateFileSizeAfter();
        validateFileTypeAfter();
        checkImageInputs();

    });

    // * Listen for form submittion
    $("form").submit((e) => {
        const isValid = validateFileSizeBefore();

        if (!isValid) e.preventDefault();

        return isValid;
    });


    //Validates the file size the user is trying to upload.
    function validateFileSizeBefore() {
        if (imgInputBefore.get(0).files.length > 0) {
            const fileSizeBefore = Math.round(Number(imgInputBefore.get(0).files[0].size) / (1024.0 * 1024.0)); // in MB
            const maxFileSizeBefore = Number(imgInputBefore.data("maxSize")) / (1024.0 * 1024.0); // in MB;

            if (fileSizeBefore > maxFileSizeBefore) {
                imgSizeWarningBefore.text("Fil størrelse på: " + fileSizeBefore.toString() + " MB er større enn maks tillatt " + maxFileSizeBefore.toString() + " MB");
                imgSizeWarningBefore.show();
                submitBtn.disabled = true;
                unvalidSizeBefore = 1;
                return false;
            }
        }
        imgSizeWarningBefore.hide();
        unvalidSizeBefore = 0;
        return true;
    }

    //Validates the file type the user is trying to upload.
    function validateFileTypeBefore() {
        if (imgInputBefore.get(0).files.length > 0) {
            var imageInputBefore = document.getElementById('imageInputBefore');
            var fileTypeBefore = imageInputBefore.files[0].type;
            var extensionBefore = fileTypeBefore.slice((Math.max(0, fileTypeBefore.lastIndexOf("/")) || Infinity) + 1);

            //Any file extension that is not in the array is indexOf(-1).
            if (supportedImageTypes.indexOf(extensionBefore) < 0) {
                imgTypeWarningBefore.text("Innvalid filtype. Vennligst velg en de følgenede filtypene: " + supportedImageTypes + ".");
                imgTypeWarningBefore.show();
                submitBtn.disabled = true;
                unvalidExtensionBefore = 1;
                return false;
            }
        }
        imgTypeWarningBefore.hide();
        unvalidExtensionBefore = 0;
        return true;   
    }



    //Validates the file size the user is trying to upload.
    function validateFileSizeAfter() {
        if (imgInputAfter.get(0).files.length > 0) {
            const fileSizeAfter = Math.round(Number(imgInputAfter.get(0).files[0].size) / (1024.0 * 1024.0)); // in MB
            const maxFileSizeAfter = Number(imgInputAfter.data("maxSize")) / (1024.0 * 1024.0); // in MB;

            if (fileSizeAfter > maxFileSizeAfter) {
                imgSizeWarningAfter.text("Fil størrelse på: " + fileSizeAfter.toString() + " MB er større enn maks tillatt " + maxFileSizeAfter.toString() + " MB");
                imgSizeWarningAfter.show();
                submitBtn.disabled = true;
                unvalidSizeAfter = 1;
                return false;
            }
        }
        imgSizeWarningAfter.hide();
        unvalidSizeAfter = 0;
        return true;
    }

    //Validates the file type the user is trying to upload.
    function validateFileTypeAfter() {
        if (imgInputAfter.get(0).files.length > 0) {
            var imageInputAfter = document.getElementById('imageInputAfter');
            var fileTypeAfter = imageInputAfter.files[0].type;
            var extensionAfter = fileTypeAfter.slice((Math.max(0, fileTypeAfter.lastIndexOf("/")) || Infinity) + 1);

            //Any file extension that is not in the array is indexOf(-1).
            if (supportedImageTypes.indexOf(extensionAfter) < 0) {
                imgTypeWarningAfter.text("Filtypen du lastet opp er ikke støttet. Vennligst velg en de følgenede filtypene: " + supportedImageTypes + ".");
                imgTypeWarningAfter.show();
                submitBtn.disabled = true;
                unvalidExtensionAfter = 1;
                return false;
            }
        }
        imgTypeWarningAfter.hide();
        unvalidExtensionAfter = 0;
        return true;
    }

    //Checks that all validation functions are accepting the users input before enabling the submit button.
    function checkImageInputs() {
        if (unvalidSizeBefore == 0 && unvalidExtensionBefore == 0 && unvalidSizeAfter == 0 && unvalidExtensionAfter == 0) {
            submitBtn.disabled = false;
        }
    }

});