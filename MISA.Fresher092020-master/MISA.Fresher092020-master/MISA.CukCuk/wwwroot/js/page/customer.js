
$(document).ready(function () {
    customerJS = new CustomerJS("Mạnh");
})

/**
 * class quản lý các function cho trang Customer
 * Author: NVMANH (25/09/2020 )
 * */
class CustomerJS extends BaseJS {
    constructor(name) {
        super();
    }
    initEvents() {
        super.initEvents();
    }
    getData() {
        this.Data = data;
    }

    //#endregion
    validateCustom() {
        return true;
    }
}


var data = [
    {
        CustomerId: 1,
        CustomerCode: "Kh0001",
        FullName: "Nguyễn Văn Mạnh",
        Gender: "Nam",
        DateOfBirth: new Date('1999-01-11'),
        Mobile: "097732433",
        Email: "manhnv229@gmail.com",
        DebitMoney: 52145145
    },
    {
        CustomerId: 2,
        CustomerCode: "Kh0002",
        FullName: "Nguyễn Văn Cường",
        Gender: "Nam",
        DateOfBirth: new Date('1999-01-11'),
        Mobile: "097732433",
        Email: "manhnv229@gmail.com",
        DebitMoney: 1400000
    },
    {
        CustomerId: 3,
        CustomerCode: "Kh0003",
        FullName: "Nguyễn Hoàng Dũng",
        Gender: "Nam",
        DateOfBirth: new Date('1999-01-11'),
        Mobile: "097732433",
        Email: "manhnv229@gmail.com",
        DebitMoney: 1500000
    },
    {
        CustomerId: 4,
        CustomerCode: "Kh0004",
        FullName: "Nguyễn Thị Mai",
        Gender: "Nam",
        DateOfBirth: new Date('1999-01-11'),
        Mobile: "097732433",
        Email: "manhnv229@gmail.com",
        DebitMoney: 25000000
    }
];