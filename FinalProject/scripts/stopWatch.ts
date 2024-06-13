export class StopWatch {
  private startTime: number;
  public elapsed = ref(0); // Make elapsed time a reactive reference
  private running: boolean = false;

  constructor() {
    this.startTime = 0;
  }

  start() {
    if (!this.running) {
      this.startTime = performance.now() - this.elapsed.value;
      this.running = true;
      requestAnimationFrame(this.update.bind(this));
    }
  }

  stop() {
    if (this.running) {
      this.elapsed.value = performance.now() - this.startTime;
      this.running = false;
    }
  }

  reset() {
    this.stop();
    this.elapsed.value = 0;
  }

  update() {
    if (this.running) {
      this.elapsed.value = performance.now() - this.startTime;
      requestAnimationFrame(this.update.bind(this));
    }
  }

  get time() {
    const totalSeconds = this.elapsed.value / 1000;
    const minutes = Math.floor(totalSeconds / 60);
    const seconds = Math.floor(totalSeconds % 60);
    const milliseconds = Math.floor(
      (totalSeconds - Math.floor(totalSeconds)) * 1000
    );
    return { minutes, seconds, milliseconds };
  }
}
