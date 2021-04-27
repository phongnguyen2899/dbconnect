/** --------------------------------------
 * Đối tượng js chứa các hàm sử dụng chung
 * Author: NVMANH ()
 * ---------------------------------------*/
var commonJS = {
    formatMoney(money) {
        return money.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.")
    },
    getDateStringYYYYMMdd(date) {
        var day = ("0" + date.getDate()).slice(-2);
        var month = ("0" + (date.getMonth() + 1)).slice(-2);
        var dateString = date.getFullYear() + "-" + (month) + "-" + (day);
        return dateString;
    }
}


Number.prototype.formatMoney = function() {
    return this.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
}
