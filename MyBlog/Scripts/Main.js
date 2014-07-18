//loading the DOM into jQuery
$(document).ready(function () {
    //here is where we put our code
    //Selecting anything with the class of 
    //'likes', when it is clicked - run a function
    $('.likes').on('click', function () {
        //When we click, run this code
        //Getting the id from data-id in our likes div
        var id = $(this).data('id');
        //Put are likes div into a variable
        var likesDiv = $(this);
        //Make a request to add a like
        $.get('/Home/Like/' + id, function (data) {
            //Replace the html of the like div that was clicked 
            // (this), with the string returned from our Get
            $(likesDiv).html(data);
        });
    });
});