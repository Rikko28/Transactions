<h2>Summary</h2>
<p>While doing this task i was learning SQLite</p>
<p>I could create unit tests (i know XUnit, mocking etc.)</p>
<p>I could create integration tests (didnt create them before, but i know that test db is required for that etc.)</p>
<p>I didn't create repositories for models, because code isn't reusable really. (but for the general purposes there should be repos (commandRepos for commands and queryRepos for queries - CQRS)</p>
<p>This app is 20% ddd. Core logic (of which there is very little) is in models. (for ddd i need IAggregate roots, valueObjects, entities and so on...), plus i tought that specifying Aggregate root for this task would be a little overkill.
What i tought that account would be root object and transactions would be account entities. So really main domain logic is Transfering money from one account to another or withdraw money, that's it. 
  Also i didnt made domain events inside domain classes (i published events in handlers) for time saving really (but wanted to show CQRS approach of multiple operations handling) - when transfering money, 
  two account ammounts must be changed, and when creating transfer transaction for account another transaction is created for account to whom that transfer is automatically.
</p>
<ol>
  <h1>
    Api docs
  </h1>
</ol>
<p>
  Everything there should be clear. Only POST Transaction:<br/><br/>
  <b>REQUIRED</b> transactionTypeId:
    <br/>
    1 - deposit,
    <br/>
    2 - withdrawal,
    <br/>
    3 - transfer.
     <br/>
  <b>REQUIRED</b> accountId.
   <br/>
  <b>REQUIRED</b> amount.
   <br/>
  <b>REQUIRED IF transactionTypeId=3</b> transferAccountId
</p>
<p>I could write this docs with summaries and xmls, but i know only theory and never done that before.(saved some time)</p>

