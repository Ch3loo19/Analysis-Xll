# Analysis-Xll
This solution compiles into a packed Excel add-in that aids in analysing panel data.
The most noteble usecase is analysing Economic Scenario Data - ESGs, which are returns files output via Monte Carlo simulations.

## How can I debug?
Hit Start to debug. This will spin-off an instance of Excel, where you can try out the functions packed in the add-in.

## How can I get hold of some test data to interact with?
Functions_Logic.Tests -> DeploymentItems -> StochReturns 1 or 2.
Load either into memory as a StochArray object. This will yield a hash, which is a pointer to the data stored in RAM.
Then pass the hash into any function in StochArray APIs.

## What is the structure of this solution?
### Function_APIs 
This project contains all the logic for interacting with Excel. 
Importers contains functions that load data into RAM, and ESG_Classes has functions that interact with this data.

### Functions_Logic
This is what the APIs call into. This contains all logic both for validating and caching data ('Logic') as well as the maths ('ESG Classes').
Cache.cs is of particular importance as it is the centrepiece for optimising excel calcs and imporving user experience.
The cache stores the output of most functions so that upon subsequent Excel recalcs, they are retrieved from cache, instead of being recalculated.

### Functions_Logic.Tests
Tests for the Functions_Logic project. 
The development of Functions_Logic was BDD-led, so you will notice that most tests are written in  a 'behavioural' style.
