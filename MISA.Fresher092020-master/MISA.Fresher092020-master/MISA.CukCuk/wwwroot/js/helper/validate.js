$(document).ready(function () {
    $('input[required]').blur(validData.initEventValidRequired);
})


var validData = {
    //#region Validate
    /**
     * Validate bắt buộc nhập
     * Author: NVMANH (30/09/2020)
     * */
    initEventValidRequired: function() {
        validData.validateRequired(this);
    },
    /**
     * Thực hiện validate các trường bắt buộc nhập
     * @param {object} input selector
     * Author: NVMANH (01/10/2020)
     */
    validateRequired: function (input) {
        var value = $(input).val();
        // Thực hiện kiểm tra xem dữ liệu có nhập hay không (khoảng trắng hoặc null...):
        if (!value || !(value && value.trim())) {
            $(input).addClass('not-required');
            $(input).attr('title', 'Trường này không dược phép để trống');
            return false;
        } else {
            $(input).removeClass('not-required');
            $(input).removeAttr('title');
            return true;
        }
    },

    /** -----------------------------------------
     * Thực hiện validate các trường bắt buộc nhập
     * @param {object} input selector
     * Author: NVMANH (01/10/2020)
     */
    validateEmail: function () {

    },

    /**
     * Thực hiện validate các trường bắt buộc nhập
     * @param {object} input selector
     * Author: NVMANH (01/10/2020)
     */
    validateFormatDate() {

    }
}