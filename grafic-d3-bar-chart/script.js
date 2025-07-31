const svg = d3.select("#chart");
const updateBtn = document.getElementById("updateBtn");

const colors = ["#4caf50", "#ff9800", "#2196f3", "#9c27b0", "#f44336"];

updateBtn.addEventListener("click", () => {
    const input = document.getElementById("dataInput").value;
    
    const parsed = input
    .split(",")
    .map(d => d.trim())
    .map(Number)
    .filter(n => Number.isInteger(n) && n >= 0);

    if (!/^\d+(,\d+)*$/.test(input.trim())) {
    alert("Input must be a comma-separated list of positive integers (e.g. 4,8,15).");
    return;
    }

  drawChart(parsed);
});
function drawChart(data) {
  svg.selectAll("*").remove();
  
  const width = parseInt(svg.style("width")) || 1000;
  const barHeight = 60;
  const barPadding = 5;
  const totalHeight = (barHeight + barPadding) * data.length;

  svg
    .attr("height", totalHeight);

  const scale = d3.scaleLinear()
    .domain([0, d3.max(data)])
    .range([0, width - 100]);

  svg.selectAll("rect")
    .data(data)
    .enter()
    .append("rect")
    .attr("x", 0)
    .attr("y", (d, i) => i * (barHeight + barPadding))
    .attr("width", d => scale(d))
    .attr("height", barHeight)
    .attr("fill", (d, i) => colors[i % colors.length]);

  svg.selectAll("text")
    .data(data)
    .enter()
    .append("text")
    .attr("x", d => scale(d) - 20)
    .attr("y", (d, i) => i * (barHeight + barPadding) + barHeight / 2 + 8)
    .attr("text-anchor", "end")
    .attr("fill", "white")
    .attr("font-size", "24px")
    .attr("font-weight", "bold")
    .text(d => d);
}
