const calcFile = (e,filePath) => {
e.preventDefault();
    $.ajax({
        url: "https://localhost:44345/api/File/GetFileStatistic?filePath=" + filePath,
        type: 'GET',
        success: function (result) {
            ps[0].innerHTML = result.LinesCnt;
            ps[1].innerHTML = result.WordsCnt;
            ps[2].innerHTML = result.DistinctWordsCnt;
            ps[3].innerHTML = result.AvgSentenceLength;
            ps[4].innerHTML = result.MaxSentenceLength;
            ps[5].innerHTML = result.LongestSeqWithoutK;
            ps[6].innerHTML = JSON.stringify(result.Colors);
        },
        error: function (error) {
            alert("error" + error);
            // Do something with the result
        }
    });
}
let form=document.getElementsByTagName("form")[0];
let inputs = document.querySelectorAll("input");
let ps = document.querySelectorAll("p");
form.addEventListener("submit", (e) => calcFile(e,inputs[0].value));
