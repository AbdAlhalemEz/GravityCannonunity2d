<?php
include('connection.php');

$result = mysqli_query($conn, "SELECT onoff FROM control WHERE id=1");

if (!$result) {
    die("Query failed: " . mysqli_error($conn));
}

$row = mysqli_fetch_assoc($result);
$onoff_value = $row['onoff'];

$onoff_int = (int)$onoff_value;
echo $onoff_int;

mysqli_close($conn);
?>
