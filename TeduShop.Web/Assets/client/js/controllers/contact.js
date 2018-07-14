var contact = {
    init: function () {
        this.registerEvent();
    },
    registerEvent: function () {
        this.initMap();
    },
    initMap: function () {
        // This example displays a marker at the center of Australia.
        // When the user clicks the marker, an info window opens.

        var uluru = { lat: parseFloat($('#hidLat').val()), lng: parseFloat($('#hidLng').val()) };
        var map = new google.maps.Map(document.getElementById('map'), {
            zoom: 17,
            center: uluru
        });

        var contentString = $("#hidAdress").val();

        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        var marker = new google.maps.Marker({
            position: uluru,
            map: map,
            title: $("#hidName").val()
        });
        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });
    }
}

contact.init();