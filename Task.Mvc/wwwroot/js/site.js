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
});

let blockBtn = document.getElementById('block-btn')
blockBtn.addEventListener('click', async function (){
    await SendData("UserManagement/Block", getCheckedUsers())
});

let unblockBtn = document.getElementById('unblock-btn')
unblockBtn.addEventListener('click', async function (){
    await SendData("UserManagement/Unblock", getCheckedUsers())
});

let deleteBtn = document.getElementById('delete-btn')
deleteBtn.addEventListener('click', async function (){
    await SendData("UserManagement/Delete", getCheckedUsers())
});

[].forEach.call(document.querySelectorAll('.user-chbox'), function(el) {
    el.addEventListener('change', function() {
        this.parentNode.parentNode.classList[this.checked ? 'add' : 'remove']('active-row');
    });
});


