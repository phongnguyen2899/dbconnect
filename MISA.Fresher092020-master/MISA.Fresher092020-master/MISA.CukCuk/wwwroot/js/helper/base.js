$(document).ready(function () {
})

/**
 * class quản lý các function cho trang Customer
 * Author: NVMANH (25/09/2020 )
 * */
class BaseJS {
    constructor(name) {
        try {
            this.getData();
            this.loadData();
            this.initEvents();

        } catch (e) {
            console.log('erro');
        }
    }

    /**
    * Gán sự kiện cho các thành phần
    * Author: NVMANH (26/09/2020)
    * */
    initEvents() {
        var self = this;
        // Gán sự kiện:
        $('#btnAdd').click(function () {
            self.FormMode = 'Add';
            $('#dlgDetail').show();
        })
        $('#btnEdit').click(this.btnEditOnClick.bind(this));
        $('#btnDelete').click(this.btnDeleteOnClick.bind(this));
        $('#btnSaveDetail').click(this.btnSaveOnClick.bind(this));
        $('#btnCancelDialog').click(function () {
            $('#dlgDetail').hide();
        })
        $('#btnClose').click(function () {
            $('#dlgDetail').hide();
        })
        $('table#tbListData').on('click', 'tr', this.rowOnClick);
        $('#btnLoad').click(this.btnReloadOnClick.bind(this));
    }
    getData() {
        this.Data = [];
    }
    /**
    * Load dữ liệu
    * Author: NVMANH (25/09/2020)
    * Edit: NVQuang (26/09/2020) - sửa lỗi tham số truyền vào
    * */
    //TODO: cần sửa lại
    loadData() {
        try {
            var self = this;
            // Đọc thông tin các cột dữ liệu:
            var fields = $('table#tbListData thead th');
            var keyId = $('table#tbListData').attr('keyId');
            var data;
            // Lấy dữ liệu:
            $.ajax({
                method: "GET",
                url: "/api/employees",
                contentType: "application/json",
                async:true
            }).done(function (res) {
                if (res) {
                    data = res;
                    // Đọc dữ liệu:
                    //$("#tbListData tbody").empty();
                    $.each(data, function (index, obj) {
                        var tr = $(`<tr></tr>`);
                        $.each(fields, function (index, field) {
                            var fieldName = $(field).attr('fieldName');
                            var value = obj[fieldName];
                            var format = $(field).attr('format');
                            if (format == "date" && value) {
                                value = new Date(value);
                                value = commonJS.getDateStringYYYYMMdd(value);
                            }
                            var td = $(`<td>` + value + `</td>`);
                            $(tr).data('keyid', obj[keyId]);
                            $(tr).data('data', obj);
                            $(tr).append(td);
                        })
                        // Binding dữ liệu lên UI:
                        //var trHTML = self.makeTrHTML(obj);
                        $("#tbListData tbody").append(tr);
                    })
                }
            }).fail(function (res) {

            });

            
        } catch (e) {
        }
    }


    /**
    * Load lại dữ liệu
    * Author: NVMANH (25/09/2020)
    * Edit: NVQuang (26/09/2020) - sửa lỗi tham số truyền vào
    * */
    btnReloadOnClick() {
        this.loadData();
    }

    /**
    * Load lại dữ liệu
    * Author: NVMANH (25/09/2020)
    * Edit: NVQuang (26/09/2020) - sửa lỗi tham số truyền vào
    * */
    btnEditOnClick() {
        this.FormMode = 'Edit';
        // Lấy thông tin bản ghi đã chọn trong danh sách:
        var recordSelected = $('#tbListData tbody tr.row-selected');
        console.log(recordSelected);
        // Lấy dữ liệu chi tiết của bản ghi đã chọn:
        //TODO: Fix thông tin để binding dữ liệu do chưa có service:
        var id = recordSelected.data('keyid');
        var objectDetail = recordSelected.data('data');
        debugger;
        console.log(id);
        // Bindding dữ liệu vào các input tương ứng trên form chi tiết:
        // Build object cần lưu:
        var inputs = $('input[fieldName]');
        $.each(inputs, function (index, input) {
            var fieldName = $(input).attr('fieldName');
            input.value = objectDetail[fieldName];
            if ($(input).attr('type') == 'date') {
                input.value = commonJS.getDateStringYYYYMMdd(objectDetail[fieldName]);
            }
        })
        // Hiển thị dialog chi tiết:
        $('#dlgDetail').show();
    }

    /**
    * Load lại dữ liệu
    * Author: NVMANH (25/09/2020)
    * Edit: NVQuang (26/09/2020) - sửa lỗi tham số truyền vào
    * */
    btnDeleteOnClick() {
        // Lấy id của bản ghi được chọn:
        var id = this.getRecordIdSelected();
        // Hiển thị thông báo xác nhận xóa:
        var result = confirm("Bạn có chắc chắn muốn xóa bản ghi này hay không?");
        debugger;
        if (result) {
            // Thực hiện xóa nếu xác nhận là ok:
            data.pop();
        }
    }
    /**
     * Thực hiện lưu dữ liệu
     * Author: NVMANH (01/10/2020)
     * */
    btnSaveOnClick() {
        debugger;
        var self = this;
        var isValid = true;
        // validate dữ liệu:
        // - Check bắt buộc nhập:
        var inputRequireds = $('input[required]');
        $.each(inputRequireds, function (index, input) {
            if (!validData.validateRequired(input)) {
                isValid = false;
            }
            //$(input).trigger('blur');
        })
        if (isValid) {
            isValid = self.validateCustom();
        }
        if (isValid) {
            // Build object cần lưu:
            var inputs = $('input[fieldName]');
            var customer = {};
            $.each(inputs, function (index, input) {
                var fieldName = $(input).attr('fieldName');
                var value = $(input).val();
                customer[fieldName] = value;
            })
            debugger
            // Gọi service thực hiện lưu dữ liệu:
            //TODO: đang fix dữ liệu dưới client
            if (self.FormMode == 'Add') {
                alert('add');
                $.ajax({
                    method: "POST",
                    url: "/api/employees",
                    data: customer,
                    contentType: "application/json"
                }).done(function (res) {
                    self.loadData();
                }).fail(function (res) {

                })
            } else {
                alert('edit');
            }

            // Xử lý sau khi lưu dữ liệu:
            $('#dlgDetail').hide();
            self.loadData();
        }
    }


    validateCustom() {
        return true;
    }
    //#region "ABC"
    //TODO: cần sửa lại
    rowOnClick() {
        //$('.row-selected').removeClass('row-selected');
        $(this).siblings().removeClass('row-selected');
        $(this).addClass('row-selected');
    }

    /**
     * Lấy id của bản ghi được chọn trên danh sách
     * Author: NVMANH (01/10/2020)
     * */
    getRecordIdSelected() {
        // Lấy thông tin bản ghi đã chọn trong danh sách:
        var recordSelected = $('#tbListData tbody tr.row-selected');
        // Lấy dữ liệu chi tiết của bản ghi đã chọn:
        var id = recordSelected.data('keyid');
        return id;
    }
    //#endregion "ABC";
}

