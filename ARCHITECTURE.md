# ARCHITECTURE.md

# Overview

Carroots is currently structured around a manager-based architecture where each manager is responsible for a specific area of the game.

The architecture was initially designed for a game jam project, prioritizing rapid development and clear separation of gameplay features. As the project evolves, selected parts of the architecture are being improved to support additional requirements such as gameplay analytics, tutorial improvements, and adaptive learning mechanisms.

The current architecture relies mainly on direct communication between managers. This approach is simple and effective for a small-scale game, but some dependencies are becoming more complex as new systems need to observe and respond to gameplay events.

---

# Core Systems

## GameManager

The GameManager controls the overall game state and progression.

Responsibilities include:

- Managing player resources (money)
- Tracking generated words
- Determining win and loss conditions
- Controlling game progression state
- Coordinating interactions between major gameplay systems

The GameManager currently communicates directly with other managers to evaluate gameplay conditions and update the game state.

---

## WordManager

The WordManager is responsible for vocabulary-related gameplay logic.

Responsibilities include:

- Loading available root words
- Randomizing word availability
- Generating gameplay word queues
- Validating constructed words
- Combining roots, prefixes, and suffixes

The WordManager acts as the source of truth for word-related information.

---

## PlantManager

The PlantManager controls the main gameplay interactions involving plants.

Responsibilities include:

- Planting root words
- Applying fertilizers
- Selling plants
- Managing player interactions with plants
- Handling plant selection and highlighting
- Managing gameplay actions related to plant growth

The PlantManager currently acts as a bridge between player actions, plant objects, game state updates, and user feedback.

---

## UIManager

The UIManager controls the presentation layer of the game.

Responsibilities include:

- Managing inventory slots
- Displaying upcoming words
- Updating resource counters
- Displaying gameplay instructions
- Managing popups and notifications

Currently, UIManager also communicates directly with gameplay systems to retrieve information required for display.

Future improvements will aim to reduce direct dependencies between UI and gameplay logic.

---

## PlantStatus

PlantStatus represents an individual plant object in the game field.

Responsibilities include:

- Maintaining the current plant word
- Tracking growth level
- Managing plant value
- Updating plant visual information
- Validating word growth through WordManager

Each plant maintains its own state while interacting with the wider gameplay system through PlantManager.

---

## ItemSlot

ItemSlot represents an inventory slot containing an available word item.

A slot can contain:

- Root words
- Prefixes
- Suffixes

Responsibilities include:

- Displaying available word items
- Handling item selection
- Removing items
- Requesting gameplay actions through PlantManager

---

# Current Architecture

The current system follows a manager-based architecture where responsibilities are divided between specialized components.

             GameManager
          ↙      ↓       ↘
         ↓       ↓        ↓
  UIManager  PlantManager  WordManager
      ↑          ↑
      |          |
  ItemSlot   PlantStatus

  
Communication between systems currently occurs mainly through direct references.

This approach works well for the current scale of the project, but some systems have become coupled because multiple components need to react to the same gameplay actions.

For example, a successful word combination may require updates to:

- The user interface
- The tutorial system
- Analytics collection
- Future adaptive gameplay systems

The introduction of selected gameplay events aims to reduce these dependencies without converting the entire project into a fully event-driven architecture.

---

# Current Architectural Characteristics

The current architecture has the following characteristics:

## Clear separation of responsibilities

Each major gameplay area has a dedicated manager.

Examples:

- Word logic is separated into WordManager.
- Plant interactions are separated into PlantManager.
- Presentation logic is handled by UIManager.

## Direct communication between systems

Managers currently communicate through direct references.

This provides a simple implementation approach but introduces dependencies between systems.

## Limited event-based communication

The current project does not use a central event system. Some future improvements will introduce events where multiple independent systems need to react to the same gameplay outcome.

---

# Future Improvements

The project is currently being modernized and expanded.

Planned improvements include:

- Improved architectural separation
- Selected event-driven communication for gameplay notifications
- Gameplay analytics collection
- Research-oriented educational analytics
- Improved tutorial progression
- Adaptive learning mechanisms