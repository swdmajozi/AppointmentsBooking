document.addEventListener('DOMContentLoaded', function () {
    const datePicker = document.getElementById('datePicker');
    const timesContainer = document.getElementById('timesContainer');
    const selectedStart = document.getElementById('selectedStart');
    const selectedEnd = document.getElementById('selectedEnd');

    function bindSlotButtons() {
        document.querySelectorAll('.slot-btn').forEach(btn => {
            if (btn.classList.contains('disabled')) return;
            btn.addEventListener('click', () => {
                document.querySelectorAll('.slot-btn').forEach(b => b.classList.remove('selected'));
                btn.classList.add('selected');
                selectedStart.value = btn.getAttribute('data-start');
                selectedEnd.value = btn.getAttribute('data-end');
            });
        });
    }

    datePicker?.addEventListener('change', function () {
        const date = this.value;
        if (!date) return;
        fetch(`/Appointment/GetAvailableSlots?date=${date}`)
            .then(r => {
                if (!r.ok) throw new Error('Network response was not ok');
                return r.text();
            })
            .then(html => {
                timesContainer.innerHTML = html;
                // clear selection
                selectedStart.value = '';
                selectedEnd.value = '';
                bindSlotButtons();
            })
            .catch(err => console.error(err));
    });

    bindSlotButtons();
});