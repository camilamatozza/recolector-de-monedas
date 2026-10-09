# Recolector de Monedas

Juego 2D de recolección de monedas hecho en Unity 6 
Proyecto del 1er Examen Parcial de **Diseño y Desarrollo de Videojuegos 2** (UCES, Tecnicatura en Programación).

**Autora:** Camila Matozza

## Cómo jugar
- Movimiento: **WASD** o **flechas**.
- Juntá las 8 monedas para ganar. Cada moneda suma puntos según su tipo:
  - Gold: 10 puntos
  - Silver: 5 puntos
  - Ruby: 25 puntos
- Al ganar aparece el panel de victoria con el puntaje final y las opciones **Jugar de nuevo** o **Volver** al menú.

## Flujo de escenas
`Splash` → `MainMenu` → `Gameplay` → (victoria) → `MainMenu` o reinicio de `Gameplay`

El Build Profile tiene las escenas en este orden: Splash (0), MainMenu (1), Gameplay (2).

## Estructura del proyecto
```
Assets/
  Scenes/    Splash, MainMenu, Gameplay
  Prefabs/
    UI/        btn_Base y variantes (Jugar, Opciones, Creditos, Salir, Volver)
    Gameplay/  Player, Coin_Base y variantes (Gold, Silver, Ruby)
  Scripts/
    UI/        UIManager, SceneLoader, SplashController
    Gameplay/  PlayerController, Collectible, GameManager, CameraFitter
```

## Decisiones de diseño
- **UIManager centralizado:** un solo script muestra y oculta los paneles por id (Main, Options, Credits, HUD, Win), en vez de tener lógica repartida en cada botón.
- **Prefabs y variantes:** un `btn_Base` con estilo común y variantes que sólo cambian color y texto; lo mismo con las monedas (`Coin_Base`, valor distinto en cada variante).
- **Desacople por eventos:** `Collectible` dispara el evento `Collected` y `GameManager` lo escucha, así la moneda no conoce al puntaje.
- **UI responsive:** Canvas Scaler en *Scale With Screen Size* (1920x1080, Match 0.5) y elementos anclados con Anchors, probado en 16:9 y 9:16.
- **CameraFitter:** ajusta el tamaño de la cámara ortográfica para que la arena completa entre en cualquier relación de aspecto.
- **Movimiento:** `Rigidbody2D.MovePosition` en `FixedUpdate`, con límites dentro de la arena.

## Flujo de trabajo en Git
- Rama `main` estable; el trabajo se hace en ramas `feature/*` o `docs/*` y se integra con Pull Request.
- Mensajes de commit con prefijos: `feat:`, `fix:`, `chore:`, `docs:`.
- Tablero Kanban en GitHub Projects para organizar tareas (Todo, In Progress, Done).

## Mejoras futuras
- Sonidos y música.
- Enemigos u obstáculos.
- Más niveles y guardado de mejor puntaje.
- Soporte de control táctil para móviles.