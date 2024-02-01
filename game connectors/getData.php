<?php
include('connection.php');

// Retrieve the top 3 scores with the name, id, and score fields from the database
$sql = "SELECT id, name, score FROM scores ORDER BY score DESC LIMIT 3";
$result = mysqli_query($conn, $sql);

// Build the CSV file
$csv = "id,name,score\n";
while ($row = mysqli_fetch_assoc($result)) {
    $csv .= $row["id"] . "," . $row["name"] . "," . $row["score"] . "\n";
}

// Return the CSV file
header("Content-Type: text/csv");
header("Content-Disposition: attachment; filename=data.csv");
echo $csv;

// Close the database connection
mysqli_close($conn);
?>
