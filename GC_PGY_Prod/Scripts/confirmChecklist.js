(() => {
    const overlay = document.createElement('div');
    overlay.id = 'confirmation-overlay';
    overlay.style = `
        position: fixed;
        top: 0; left: 0;
        width: 100vw;
        height: 100vh;
        background: rgba(0, 0, 0, 0.7);
        z-index: 9998;
        display: flex;
        justify-content: center;
        align-items: center;
    `;

    const modal = document.createElement('div');
    modal.style = `
        background: #ffffff;
        border: 4px solid #0078d4;
        padding: 25px 30px;
        width: 400px;
        font-family: sans-serif;
        border-radius: 10px;
        z-index: 9999;
        box-shadow: 0 0 20px rgba(0,0,0,0.5);
    `;

    modal.innerHTML = `
        <h2 style='color:#0078d4; margin-top:0;'>Pre-Test Checklist</h2>
        <p><label><input type='checkbox' id='emailMasked'> Email is masked</label></p>
        <p><label><input type='checkbox' id='emailSent'> Test email has been sent</label></p>
        <button id='confirmButton' style='margin-top: 10px; padding: 8px 12px; background-color: #0078d4; color: white; border: none; border-radius: 5px; cursor: pointer;'>Confirm</button>
        <p id='warning' style='color: red; display: none; margin-top: 10px;'>Please check all items before proceeding.</p>
    `;

    overlay.appendChild(modal);
    document.body.appendChild(overlay);

    const closePopup = (wasChecked) => {
        window.__confirmationDone = true;
        window.__checklistStatus = wasChecked ? 'success' : 'fail';
        document.getElementById('confirmation-overlay')?.remove();
        console.log(wasChecked ? 'Pre-test checklist marked' : 'Pre-test checklist not marked');
    };

    document.getElementById('confirmButton').onclick = () => {
        const emailMasked = document.getElementById('emailMasked').checked;
        const emailSent = document.getElementById('emailSent').checked;

        if (emailMasked && emailSent) {
            closePopup(true);
        } else {
            document.getElementById('warning').style.display = 'block';
        }
    };

    // Auto close after 30 seconds
    setTimeout(() => {
        if (!window.__confirmationDone) {
            const emailMasked = document.getElementById('emailMasked').checked;
            const emailSent = document.getElementById('emailSent').checked;
            closePopup(emailMasked && emailSent);
        }
    }, 30000);
})();
