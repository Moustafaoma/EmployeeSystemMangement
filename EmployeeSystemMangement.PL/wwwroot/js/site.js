// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



    $(document).ready(function(){
        $('input[name="name"]').on('keyup', function () {
            var query = $(this).val();
            if (query.length > 0) { // Only make a request if there's some input
                $.ajax({
                    url: 'Index', // Update this URL to match your server route
                    type: 'post',
                    data: { name: query },
                    success: function (response) {
                        // Handle the response here
                        console.log(response);
                        // Example of updating a results div
                        $('#results').html(response);
                    },
                    error: function (xhr) {
                        // Handle errors here
                        console.error(xhr);
                    }
                });
            } else {
                $('#results').empty(); // Clear results if input is empty
            }
        });
});




