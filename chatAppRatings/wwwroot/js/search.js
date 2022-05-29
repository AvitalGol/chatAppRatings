$(function () {
    $('form').submit(e => {
        e.preventDefault();

        const q = $('#search').val();

        $('tbody').load('/Ratings/Search2?query='+q);
    })
})