
const selectors = {
    nameInput: '#nameinput',
    nameInputEcho:"#nameinputecho"
}

$(document).ready(function () {
    $(selectors.nameInput).on("input", function () {
        let inputValue = $(selectors.nameInput).val();
        setText(selectors.nameInputEcho, inputValue);
    })
})


function setText(selector, text){
    $(selector).text(text);
}