


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
            
            var topics = JSON.parse(this.responseText);

            for (var i = 0; i < topics.length; i++) {
                console.log(topics[i]);
                for (var j = 0; j < topics[i].repeatDates.length; j++) {
                    calendar.addEvent({
                        title: topics[i].name,
                        start: topics[i].repeatDates[j],
                        url: './Subjects/Topic?topicId=' + topics[i].id
                        
                    });
                }

            }
            calendar.render();
        }
    };

    request.open('GET', '/api/GetTopics', true);
    request.send(null);
    
});