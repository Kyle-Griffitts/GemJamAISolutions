// Geolocation helper function
window.getGeolocation = () => {
    return new Promise((resolve, reject) => {
        if (!navigator.geolocation) {
            reject('Geolocation is not supported by your browser');
            return;
        }

        navigator.geolocation.getCurrentPosition(
            (position) => {
                const coords = {
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude
                };
                console.log('Location shared - Latitude:', coords.latitude, 'Longitude:', coords.longitude);
                resolve(coords);
            },
            (error) => {
                let errorMessage = 'Unknown error';
                switch (error.code) {
                    case error.PERMISSION_DENIED:
                        errorMessage = 'User denied the request for Geolocation';
                        break;
                    case error.POSITION_UNAVAILABLE:
                        errorMessage = 'Location information is unavailable';
                        break;
                    case error.TIMEOUT:
                        errorMessage = 'The request to get user location timed out';
                        break;
                }
                console.error('Geolocation error:', errorMessage);
                reject(errorMessage);
            }
        );
    });
};
