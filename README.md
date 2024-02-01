# Physics Learning Cannon Game

## Project Description

The Physics Learning Cannon Game is an educational project aimed at contributing gaming to the field of education. Designed for children, this game teaches the basics of physics through an engaging cannon-shooting experience. Players use a cannon to launch balls with different masses, velocities, and angles to destroy obstacles in diverse environments such as the moon, Earth, and Mars. The goal is to destroy as many obstacles as possible to achieve the highest score using the least number of balls, securing a spot on the leaderboard before the time for each level expires.

## Table of Contents

- [Installation](#installation)
- [Gameplay](#gameplay)
- [Environments](#environments)
- [Scoring](#scoring)
- [Leaderboard](#leaderboard)
- [Connectors](#connectors)
- [Database Setup](#database-setup)
- [Contributing](#contributing)
- [License](#license)

## Installation

To play the Physics Learning Cannon Game, follow these installation steps:

1. Clone the repository: `git clone https://github.com/your-username/physicslearningcannon.git`
2. Import the database: Execute `database.sql` in your MySQL localhost to set up the required tables.
3. Open the project in Unity.
4. Build and run the game.

Now, embark on an educational journey in the world of physics!

## Gameplay

In the Physics Learning Cannon Game, players must:

1. **Shoot Balls**: Use the cannon to launch balls with different masses, velocities, and angles.
2. **Destroy Obstacles**: Strategically aim to destroy obstacles in various environments.
3. **Achieve High Scores**: Maximize your score by using the least number of balls.
4. **Complete Levels**: Progress through different levels on the moon, Earth, and Mars.

## Environments

Explore the following environments in the game:

- **Moon**: Experience low gravity and unique physics challenges.
- **Earth**: Navigate through obstacles in a familiar gravitational environment.
- **Mars**: Overcome challenges in the lower gravity of the red planet.

## Scoring

Score points based on the mass, velocity, and angle of the balls used to destroy obstacles. The game rewards precision and efficiency.

## Leaderboard

Compete for a spot on the leaderboard by achieving the highest score with the least number of balls. Aim to be the top physics expert in each environment!

![game play](https://github.com/AbdAlhalemEz/GravityCannonunity2d/blob/main/gameplay.gif)

## Connectors

The PHP files in the Connectors folder are used to connect the Cannon game to the database. They facilitate signing in, retrieving leaderboard scores, and tracking student playtime and scores.

## Database Setup

1. Import the `database.sql` file into your MySQL localhost to set up the required tables.
2. Change the `connection.php` file in the Connectors folder to your localhost, username, and password.

```php
// Example connection.php
<?php
$servername = "localhost";
$username = "your_username";
$password = "your_password";
$dbname = "your_database_name";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
?>
