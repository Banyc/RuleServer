# Rule Server

## Projects

-   `RuleWeb` - visual rule editor
-   `RuleEngine` - core engine
-   `RuleServer` - service wrapper containing `RuleEngine`

## Index

The index is a system to lookup qualified rules promptly with the given keys. The keys are the values of a given field. The given field is specified by users.

Users specify which fields to be indexed in `IndexedParameters`.

When building an index, the system will inspect the min-terms of each rule. With the info, the system determines whether to add this rule to the index. (`RuleEngineLoadRulesStaticHelpers.UpdateIndex(newGroup);`)

When a JSON request comes, the system will first get the values of indexed fields from the JSON request. Then, the system takes the values to match the index. The index will return which rules to calculate. (`ruleGroup.GetMatchedRules(symbolTable);`)

### Rule's qualification to be indexed

In each term of a rule's min-terms, if the term has `indexedField == value`, then the rule is indexed under the key `value`.

If any term does not have `indexedField == value`, then the rule is irrelevant to `indexedField`. Since there is an uncertainty of where the rule is qualified, the rule should be indexed under all keys of `indexedField`.

### User tips

Don't index a field if many rules are irrelevant to the field.

## References

