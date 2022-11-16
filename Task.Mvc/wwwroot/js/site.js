let mainCheckbox = document.getElementById("main-checkbox")
mainCheckbox.addEventListener('change', function (e) {
    let boxes = document.querySelectorAll("table input[type='checkbox']")
    for (let i = 0; i < boxes.length; i++) {
        boxes[i].checked = mainCheckbox.checked === true;
    }
    let allCheckboxes = document.querySelectorAll('.user-chbox');
    allCheckboxes.forEach(c => {
        c.parentNode.parentNode.classList[this.checked ? 'add' : 'remove']('active-row');
    })
})

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
}

let blockBtn = document.getElementById('block-btn')
blockBtn.addEventListener('click', async function (){
    await SendData("UserManagement/Block", getCheckedUsers())
})

let unblockBtn = document.getElementById('unblock-btn')
unblockBtn.addEventListener('click', async function (){
    await SendData("UserManagement/Unblock", getCheckedUsers())
})

let deleteBtn = document.getElementById('delete-btn')
deleteBtn.addEventListener('click', async function (){
    await SendData("UserManagement/Delete", getCheckedUsers())
})

async function SendData(input, data) {
    if (getCheckedUsers().length > 0){
        await fetch(input, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                "Content-Type": "application/json"
            }
        });

        document.location.reload();
    }
}

[].forEach.call(document.querySelectorAll('.user-chbox'), function(el) {
    el.addEventListener('change', function() {
        this.parentNode.parentNode.classList[this.checked ? 'add' : 'remove']('active-row');
    });
});


