<template>
  <v-app>
    <v-container class="d-flex justify-center align-center mt-5">
      <v-row>
        <v-col cols="12">
          <h1 class="display-1 text-center">Welcome to the Game!</h1>
        </v-col>
        <v-col cols="12" class="d-flex justify-center">
          <div ref="gameContainer" style="width: 800px; height: 600px; margin: 0 auto;"></div>
        </v-col>
        <v-col cols="12" class="d-flex justify-center">
          <v-btn @click="router.push('/')" color="secondary"> Home Page </v-btn>
          <v-btn @click="togglePause" class="ml-1" color="primary"> {{ isPaused ? 'Resume' : 'Pause' }}</v-btn>
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>


<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";
import { Engine, Actor, Color, CollisionType, vec } from "excalibur";

const router = useRouter();

const gameContainer = ref<HTMLDivElement | null>(null);
const isPaused = ref(false);

let game: Engine | null = null;
let colliding = false;

onMounted(() => {

startGame();

});

onUnmounted(() => {
  if (game) {
    game.stop();
    if (gameContainer.value && gameContainer.value.firstChild) {
      gameContainer.value.removeChild(gameContainer.value.firstChild);
    }
  }
});

function startGame() {
  if (gameContainer.value) {
    const canvas = createCanvas(800, 600);
    gameContainer.value.appendChild(canvas);

    game = createGame(canvas);
  }

  const paddle = createPaddle();

  paddle.body.collisionType = CollisionType.Fixed;
  game?.add(paddle);

  game?.input.pointers.primary.on("move", (evt) => {
    paddle.pos.x = evt.worldPos.x;
  });

  const ball = createBall();
  game?.add(ball);



  const bricks = createBricks();

  // On collision remove the brick, bounce the ball
  colliding = false;
  ball.on("collisionstart", function (ev) {
    if (bricks.indexOf(ev.other) > -1) {
      // kill removes an actor from the current scene
      // therefore it will no longer be drawn or updated
      ev.other.kill();

      // Increase ball speed with each killed brick 
      ball.vel = ball.vel.scale(1.2);
    }

    var intersection = ev.contact.mtv.normalize();

    if (!colliding) {
      colliding = true;
      if (Math.abs(intersection.x) > Math.abs(intersection.y)) {
        ball.vel.x *= -1;
      }
      else {
        ball.vel.y *= -1;
      }
    }
  });

  ball.on("collisionend", () => {
    colliding = false;
  })

  ball.on("exitviewport", () => {
    console.log("Game over!");
    restartGame();
  })


  game?.start();
}

function restartGame() {
  if (game) {
    game.stop();
    if (gameContainer.value && gameContainer.value.firstChild) {
      gameContainer.value.removeChild(gameContainer.value.firstChild);
    }
  }
  startGame();
}

function createCanvas(width: number, height: number): HTMLCanvasElement {
  const canvas = document.createElement("canvas");
  canvas.width = width;
  canvas.height = height;
  return canvas;
}

function createGame(canvas: HTMLCanvasElement): Engine {
  return new Engine({
    canvasElement: canvas,
    width: 800,
    height: 600,
  });
}

function createPaddle(): Actor {
  return new Actor({
    x: 150,
    y: game?.drawHeight - 40,
    width: 200,
    height: 20,
    color: Color.Chartreuse,
  });
}

function createBall(): Actor {

  const ball = new Actor({
    x: 100,
    y: 300,
    radius: 10,
    color: Color.Red,
  });

  const ballSpeed = vec(100, 100);
  setTimeout(() => {
    ball.vel = ballSpeed;
  }, 1000);

  ball.body.collisionType = CollisionType.Passive;

  ball.on("postupdate", () => {
    // If the ball collides with the left side
    // of the screen reverse the x velocity
    if (ball.pos.x < ball.width / 2) {
      ball.vel.x = ballSpeed.x;
    }

    // If the ball collides with the right side
    // of the screen reverse the x velocity
    if (ball.pos.x + ball.width / 2 > game?.drawWidth) {
      ball.vel.x = ballSpeed.x * -1;
    }

    // If the ball collides with the top
    // of the screen reverse the y velocity
    if (ball.pos.y < ball.height / 2) {
      ball.vel.y = ballSpeed.y;
    }
  });

  return ball;
}

function createBricks(): Actor[] {
  // Build Bricks

  // Padding between bricks
  const padding = 20; // px
  const xoffset = 65; // x-offset
  const yoffset = 20; // y-offset
  const columns = 5;
  const rows = 3;

  const brickColor = [Color.Violet, Color.Orange, Color.Yellow];

  // Individual brick width with padding factored in
  const brickWidth = game?.drawWidth / columns - padding - padding / columns; // px
  const brickHeight = 30; // px
  const bricks: Actor[] = [];
  for (let j = 0; j < rows; j++) {
    for (let i = 0; i < columns; i++) {
      bricks.push(
        new Actor({
          x: xoffset + i * (brickWidth + padding) + padding,
          y: yoffset + j * (brickHeight + padding) + padding,
          width: brickWidth,
          height: brickHeight,
          color: brickColor[j % brickColor.length],
        })
      );
    }
  }

  bricks.forEach(function (brick) {
    // Make sure that bricks can participate in collisions
    brick.body.collisionType = CollisionType.Active;

    // Add the brick to the current scene to be drawn
    game?.add(brick);
  });

  return bricks;
}

const togglePause = () => {
  if (game) {
    if (isPaused.value) {
      game.start();
    } else {
      game.stop();
    }
    isPaused.value = !isPaused.value;
  }
};

</script>
