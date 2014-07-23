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

    //adding a comment, bind a submit event to the addComment form 
    $('.addComment form').on('submit', function (event) {
        //stop the form from submitting normallly
        event.preventDefault();
        //put this (the form) into a variable
        var theForm = $(this);
        //do the AJAX POST, ust the serialize command to make a string of data
        $.post('/home/addComment', $(this).serialize(), function (data) {
            theForm.parent().prepend(data);
            //Clear the input fields
            theForm.find('#Name').val("");
            theForm.find('#Body').val("");
        });
    });
});