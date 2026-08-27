# EVENTS.md

## Overview

The Carroots project follows a hybrid architecture where core gameplay is driven through direct interactions between game objects, while important gameplay outcomes are communicated through events.

Rather than making the entire project event-driven, events are introduced only where they provide a clear architectural benefit. This keeps the gameplay code simple while allowing independent systems to react to important changes without creating unnecessary dependencies.

For example, planting a seed or applying a fertilizer remains a direct gameplay action. However, after the action has been completed, other systems may need to respond to the outcome. Instead of the gameplay system directly notifying each of these systems, an event can communicate what occurred.

## Why introduce events?

Originally, most gameplay interactions in Carroots relied on direct references between managers. While this approach works well for a small project, extending the game becomes increasingly difficult when multiple systems need to respond to the same gameplay action.

For example, a successful word combination may need to:

- update the user interface,
- advance the tutorial,
- record learning analytics,
- unlock future achievements,
- or support adaptive gameplay mechanisms.

Without events, the gameplay code would gradually become responsible for directly communicating with every system interested in that action.

By introducing events, gameplay systems report that something has occurred, while other systems can independently decide whether they need to respond.

## Commands and Events

Within Carroots, it is useful to distinguish between commands and events.

A command represents an action requested by a system. Examples include planting a root, applying a fertilizer, or harvesting a plant.

An event represents the outcome of an action after it has already occurred.

This distinction separates gameplay execution from gameplay observation. Gameplay systems remain responsible for performing actions, while other systems can observe the results without becoming directly connected to the internal gameplay logic.

## Events as gameplay observations

The event system is designed around meaningful gameplay actions rather than individual function calls.

Instead of notifying that a specific method has executed, events describe what occurred from the perspective of the game world.

For example, rather than communicating that the `GrowWord()` method was called, an event can describe that a player attempted to combine two word components and whether the resulting combination was successful.

Considering events from the player's perspective rather than from the code implementation perspective makes them reusable across multiple systems.

## Events and Learning Analytics

One of the primary motivations for introducing events is to support future learning analytics.

Rather than recording isolated technical actions, the game should capture meaningful learning interactions.

For example, attempting to combine a stem with a prefix or suffix represents a learning decision made by the player. Recording the complete interaction allows future analytics to examine not only whether the answer was correct, but also the process that led to that outcome.

The same gameplay event can therefore support multiple systems, including analytics, tutorials, adaptive gameplay mechanisms, and future research, without requiring modifications to the core gameplay logic.

## Current Scope

The event system is introduced incrementally.

The initial focus is on gameplay events that represent meaningful player interactions. Additional events may be introduced later when they provide clear value in simplifying communication between systems.

This approach avoids unnecessarily converting the entire project into an event-driven architecture while still providing the flexibility required for future extensions.