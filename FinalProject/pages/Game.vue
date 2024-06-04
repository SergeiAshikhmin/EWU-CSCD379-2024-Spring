<template>
	<v-app>
		<v-container>
			<v-row>
				<v-col cols="12">
					<h1>Home Page, right? </h1>
          <v-btn @click="router.push('/')"> Home Page </v-btn>
				</v-col>
			</v-row>

			<div ref="gameContainer"></div>

		</v-container>
	</v-app>
</template>


<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";
import { Engine, Actor, Color, CollisionType, vec } from "excalibur";

const router = useRouter();

const gameContainer = ref<HTMLDivElement | null>(null);

let game: Engine | null = null;

onMounted(() => {
  if (gameContainer.value) {
    const canvas = document.createElement("canvas");
    canvas.width = 800;
    canvas.height = 600;
    gameContainer.value.appendChild(canvas);
    game = new Engine({
      canvasElement: canvas,
      width: 800,
      height: 600,
    });
  }

  const paddle = new Actor({
    x: 150,
    y: game.drawHeight - 40,
    width: 200,
    height: 20,
    color: Color.Chartreuse,
  });

  paddle.body.collisionType = CollisionType.Fixed;
  game?.add(paddle);

  game?.input.pointers.primary.on("move", (evt) => {
    paddle.pos.x = evt.worldPos.x;
  });

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
  game?.add(ball);

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

  // On collision remove the brick, bounce the ball
  let colliding = false;
  ball.on("collisionstart", function (ev) {
    if (bricks.indexOf(ev.other) > -1) {
      // kill removes an actor from the current scene
      // therefore it will no longer be drawn or updated
      ev.other.kill();
    }

    var intersection = ev.contact.mtv.normalize();

    if(!colliding) {
      colliding = true;
      if(Math.abs(intersection.x) > Math.abs(intersection.y)) {
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
    alert("Game over!");
  
  })

  game?.start();
});

onUnmounted(() => {
  if (game) {
    game.stop();
    if (gameContainer.value && gameContainer.value.firstChild) {
      gameContainer.value.removeChild(gameContainer.value.firstChild);
    }
  }
});

</script>