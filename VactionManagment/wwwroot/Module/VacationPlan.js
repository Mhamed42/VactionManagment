$(document).ready(() => {
    GetVacationType()

});
FindEmployee = () => {
    if ($('#employeeName').val() =='') {
        $('#ddlEmployees').html('<option value="">----------------Not Found (Enter Name)----------------</option>');

    } else {

        $.ajax({
            url: '/api/VacationPlanApi/' + $('#employeeName').val(),
            Method: 'GET',
            cache: false,
            success: (data) => {
                let Employee = '';
                Employee += `<option value="">----------------Item Found (${data.length})----------------</option>`;
                for (x in data) {
                    Employee += `<option value="${data[x].id}">${data[x].id}-${data[x].name}</option>`;
                }
                $('#ddlEmployees').html(Employee);
            }


        });


    }

   

}
GetVacationType = () => {
    //if ($('#employeeName').val() == '') {
    //    $('#ddlEmployees').html('<option value="">----------------Not Found (Enter Name)----------------</option>');

    //} else {

    $.ajax({
        url: '/VacationPlans/GetVacationType/',
        //+ $('#employeeName').val(),
        Method: 'GET',
        cache: false,
        success: (result) => {
            let Vacation = '';
            Vacation += `<option value="">----------------Select Vacation (${result.length})----------------</option>`;
            for (x in result) {
                Vacation += `<option value="${result[x].id}"style=" color:#ffff;background-color:${result[x].backgroundColor};">${result[x].id}-${result[x].vacationName}=======Days:-(${result[x].numberDays})</option>`;
            }
            $('#ddlVacationType').html(Vacation);
        }

    });
}