<script language="javascript">
    $(document).ready(
    function () {
            var mychart = new Chart(document.getElementById("chart"), {
        type: 'doughnut',
    data: {
        labels: ['@Model.ChoiceA', '@Model.ChoiceB'],
    datasets: [{
        label: 'Votes',
    backgroundColor: ["cyan", "pink"],
    data: [@Model.CountA, @Model.CountB]
                  }]
               },
    options: {
        responsive: false,
    plugins: {
        labels: {
        render: 'value',
    fontSize: 32,
    fontStyle: 'bold',
                     }
                  },
    legend: {
        display: false,
    position: 'bottom'
                  }
               }
            });

    $.ajaxSetup({cache: false });
    setInterval(
    function () {
        $.getJSON(
            "/Lesson12/Opinion/GetOutcome/@Model.PollGUID",
            function (data) {
                mychart.data.datasets[0].data = data;
                mychart.update();
            }
        );
               },
    2000
    );

         }
    );
</script>
