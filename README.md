( State Pattern Example - Click Seeker Bot )

The State Pattern uses a 'Finite State Machine' to store possible states and handle the transitions
on how an entity can move from one state to the next. states can be constrained to a particular object so that
it behaves differently depending on what's happening inside of the game.

Click Seeker Bot States:
The scene has a Bot with some important classes:
"Bot_StateMachine.cs" - This is the Bot's State Machine. It holds all the possible states
"Bot_IdleState.cs" - This State defines the Bot's behaviour while in Idle (motionless) state
"Bot_SearchState.cs" - This State defines the Bot's behaviour while in Searching (seeking target) state
"Bot_FoundState.cs" - This State defines the Bot's behaviour while in Found (reaching target) state

Each State needs to implement the following methods:
"Enter" - Gets called when the state is entered. Use this to set up an state
"LogicUpdate" - this simulates Update() ticks during the state
"PhysicsUpdate" - this simulates FixedUpdate() ticks during the state
"Exit" - gets called when the state is transitioning out to a new state. Use this to clean anything
that might had changed when the state was entered.

Press Play and left-click over the ground. There's a TargetAssigner gameObject that set targets for
the Bot to responds to them according to its current state:
- When a target is setted, the Bot'll transition from 'Idle' to 'Searching' and move towards the target.
- If it is currently 'Searching' and gets to the target, it'll change to 'Found' state.
- After a short waiting time, it'll transition back to 'Idle'.

Based on the explanation and concepts from https://gameprogrammingpatterns.com/state.html
