


document.addEventListener('DOMContentLoaded', function () {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['list'],
        timeZone: 'UTC',
        defaultView: 'listMonth',
        eventColor: 'orange',

        eventMouseEnter: function (info) {
            var topicId = info.event.id;
            var editUrl = "./Subjects/Topic?topicId=" + topicId;
            
            
            var editBtn = "<a class='cal-hover-btn btn btn-sm btn-secondary ml-1 float-right text-white' href='" + editUrl + "'>Edit</a>";
            $(".fc-list-item-title", info.el).append(editBtn);

            if (info.event.extendedProps.isrepeated != true) {
                var reviewUrl = "./Review/Topic?topicId=" + topicId;
                var reviewBtn = "<a class='cal-hover-btn btn btn-sm btn-primary ml-1 float-right text-white' href='" + reviewUrl + "'>Review</a>";
                $(".fc-list-item-title", info.el).append(reviewBtn);
            }
            
        },

        eventMouseLeave: function (info) {
            //$(".fc-list-item-title", info.el).remove(".cal-hover-btn");           
            $(".cal-hover-btn", info.el).remove();
        },

        // customize the button names,
        // otherwise they'd all just say "list"
        views: {
            listMonth: { buttonText: 'Month' },
            listYear: { buttonText: 'Year' },
        },

        header: {
            left: 'title',
            center: 'prev,next',
            right: 'listMonth,listYear'
        }
    });
    calendar.render();



    var request = new XMLHttpRequest();
    request.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            
            var topics = JSON.parse(this.responseText);

            for (var i = 0; i < topics.length; i++) {
                for (var j = 0; j < topics[i].repeatDates.length; j++) {
                    var backColor = (topics[i].repeatDates[j].repeated) ? "green" : "orange";
                    if ((Date.parse(topics[i].repeatDates[j].date) < Date.now()) && !(topics[i].repeatDates[j].repeated)) {
                        backColor = "red";
                    }
                    calendar.addEvent({
                        title: topics[i].name,
                        id: topics[i].id,
                        start: topics[i].repeatDates[j].date,
                        //url: './Subjects/Topic?topicId=' + topics[i].id,
                        isrepeated: (topics[i].repeatDates[j].repeated) ? true : false,
                        backgroundColor: backColor,

                        
                    });
                }

            }
            calendar.render();
        }
    };

    request.open('GET', '/api/GetTopics', true);
    request.send(null);
    
});