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