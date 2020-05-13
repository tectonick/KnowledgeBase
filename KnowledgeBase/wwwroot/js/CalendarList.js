


document.addEventListener('DOMContentLoaded', function () {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['list'],
        timeZone: 'UTC',
        defaultView: 'listMonth',
        eventColor: 'orange',

        // customize the button names,
        // otherwise they'd all just say "list"
        views: {
            listWeek: { buttonText: 'list week' },
            listMonth: { buttonText: 'list month' },
            listYear: { buttonText: 'list year' },
        },

        header: {
            left: 'title',
            center: 'prev,next',
            right: 'listWeek,listMonth,listYear'
        }
    });
    calendar.render();



    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            
            var themes = JSON.parse(this.responseText);

            for (var i = 0; i < themes.length; i++) {
                console.log(themes[i]);
                for (var j = 0; j < themes[i].repeatDates.length; j++) {
                    calendar.addEvent({
                        title: themes[i].name,
                        start: themes[i].repeatDates[j],
                        url: './Subjects/Theme?themeId=' + themes[i].id
                        
                    });
                }

            }
            calendar.render();
        }
    };

    request.open('GET', '/api/GetThemes', true);
    request.send(null);
    
});