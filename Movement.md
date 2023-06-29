<h1 align="center">Movement</h1>



<br>
<h2 align="center">Base movement</h2>
<p align="center">
  Using WSAD or arrows character will move in the input direction with current speed and acceleration.<br>
  Speed is changed to walk, run ... and used as a multiplier to adjust players velocity.<br>
  To smooth out movement acceleration is applied with a setting that can be changed by ice platform to achieve slippery effect.
</p>


<br>
<h2 align="center">Jumping</h2>
<p align="center">
  Player can jump 2 times, this is represented as jump count. When player jumps this variables is incremented, and reset to 0 when player is grounded.<br>
  Most of the time player will only be allowed to jump while grounded, here it is maily determined by jump count.
  Simply, jump will only happen if jump count is less than 2, player doesn't have to be grounded.
</p>


<br>
<h2 align="center">Sliding</h2>
<p align="center">
  Slide increases speed, decreases height, but you can't look around while sliding.<br>
  In order to get back to base movement, player has to wait for slide to end or jump.
</p>


<br>
<h2 align="center">Walls</h2>
<p align="center">
  Player can walljump only on special JumpWall platform.
  Upon entering this platform, player will stick to it and get one jump, to exit this state player has to jump.
  Leaving wall will restrict players movement untill grounded, this forces player to take a second to make a correct move.
</p>


<h3 align="center">
  <a href="README.md">ReadMe</a>
</h3>
