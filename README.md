# PlanetGorgon
Planet Gorgon is an ambitious project by a niave man who had no idea what he was getting in to. The development of this game is currently underway and will be used as a framework for future games to come.

## Multiplayer
Below is an example of the multiplayer capabilities currently in PlanetGorgon. Key components currently include:
- Utilizes DarkRift2 (https://darkriftnetworking.com/)
- x,y,z position syncronization between clients
- x,y,z rotational syncronization between clients
- Animation state serialization and syncronization (prevents client side physics calculations when the animation state can be captured on the client side and transmitted in the serialized payload as a single byte - ie jumping).
- Additional animation parameters can be captured such as an attack or interaction animation.

[![Alt text](https://img.youtube.com/vi/t34eT3LRPtw/mqdefault.jpg)](https://www.youtube.com/watch?v=t34eT3LRPtw)

## TODO List
- [x] Make a TODO List
