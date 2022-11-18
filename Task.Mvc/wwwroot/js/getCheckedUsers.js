function getCheckedUsers() {
    let checkboxes = document.getElementsByClassName('user-chbox')
    let checked = []
    for (let index = 0; index < checkboxes.length; index++) {
        if (checkboxes[index].checked) {
            checked.push(checkboxes[index].value);
        }
    }

    let checkedUsers = []
    let a = new Set(checked);
    a.forEach(x => checkedUsers.push(x));

    return checkedUsers
};