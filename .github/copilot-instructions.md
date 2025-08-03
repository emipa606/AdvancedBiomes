# GitHub Copilot Instructions for Advanced Biomes (Continued)

## Mod Overview
**Advanced Biomes (Continued)** is a mod designed to expand RimWorld's environmental diversity by introducing five new biomes, each with unique challenges and traits. Originally created by "Hey my team rules!" and continued by the current developer, the mod aims to provide players with novel experiences while navigating the complex and hazardous terrains.

## Purpose
The mod's primary goal is to enhance gameplay by adding new biomes that interact with existing systems in RimWorld. It offers fresh strategic elements and narrative dynamics within the game, leveraging both visual and mechanical features to immerse the player in novel ecological environments.

## Key Features and Systems
- **New Biomes:**
  - **Poison Forest:** A dense, toxic woodland with unique weather effects that increase colonists' toxicity.
  - **Savanna:** Open grasslands ideal for hunting and ranged combat.
  - **Volcano:** A fiery landscape with periodic volcanic eruptions that block sunlight.
  - **Wetland:** A waterlogged area presenting construction challenges and supporting distinct flora.
  - **Wasteland:** A radioactive desert with minimal viable resources.
- **Custom Weather Effects:** including "Poison Spores" that influence player strategy.
- **Compatibility Additions:**
  - Options for altering biome probability during world generation.
  - Integration with Vanilla Fishing Expanded.
  - Support for multiple languages, including Chinese and French.
  - Mod options for specific settings, like the Volcano Variety.

## Coding Patterns and Conventions
- **C# Conventions:** Ensure adherence to standard C# coding styles, including:
  - PascalCase for class names and public methods.
  - camelCase for private fields and parameters.
  - Consistent use of braces `{}` for conditionals and loops even for single-line statements.
- **File Naming:** Reflect the purpose clearly (e.g., `BiomeWorker_[BiomeName].cs`).
- **Class Definitions:** Separate each logical component (biomes, settings, etc.) into distinct classes for modularity.

## XML Integration
- XML files define various game objects (e.g., biomes, animals, plants). Ensure XML tags conform to RimWorld standards.
- Use <ThingDef>, <BiomeDef>, and other relevant tags appropriately to introduce new content.
- Verification of XML syntax is critical to prevent runtime errors.

## Harmony Patching
- Implement Harmony patches to extend or modify existing game behavior while ensuring compatibility with other mods.
- Focus on postfix and transpiler methods carefully to avoid disrupting game logics.
- Document Harmony patches with comments specifying their purpose and affected areas.

## Suggestions for Copilot
- **Class Definitions:** Prompt for class boilerplates, ensuring adherence to the mod's naming conventions and structure.
- **Harmony Patches:** Offer templates for postfix and transpiler methods, including typical usage scenarios.
- **XML Entries:** Provide sample XML snippets to facilitate the addition of new game objects.
- **Common Errors:** Highlight known issues related to decompiled code or patching conflicts, suggesting inline error checks.
- **Refactoring Assistance:** Recommend improvements for legacy code segments, especially those from decompilation.

By following these instructions, contributors can efficiently extend and maintain Advanced Biomes while ensuring consistency and quality in both code and gameplay experience.


This markdown file provides a detailed guide on how to work with the Advanced Biomes mod codebase. It outlines the purpose of the mod, its key features, C# coding standards, XML integration, Harmony patching recommendations, and suggestions to help Copilot generate relevant and useful code.
