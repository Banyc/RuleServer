grammar RuleCondition;

start
    : expr EOF
    ;

expr
    : OPEN_PAR_SYMBOL expr CLOSE_PAR_SYMBOL                                              # exprPar
    | (PLUS_OPERATOR | MINUS_OPERATOR) expr                                            # exprSign  // review: to check if it conflict with exprAdd
    | LOGICAL_NOT_OPERATOR expr                                                                  # exprNot
    | expr op = (MULT_OPERATOR | DIV_OPERATOR)  expr                                   # exprMul
    | expr op = (PLUS_OPERATOR | MINUS_OPERATOR) expr                                  # exprAdd
    | expr op = compOp expr                                                            # exprCompare
    | expr op = LOGICAL_AND_OPERATOR expr                               # exprAnd
    | expr op = LOGICAL_OR_OPERATOR expr                                 # exprOr
    | atom                                                                             # exprAtom
    ;

compOp
    : EQUAL_OPERATOR
    | GREATER_OR_EQUAL_OPERATOR
    | GREATER_THAN_OPERATOR
    | LESS_OR_EQUAL_OPERATOR
    | LESS_THAN_OPERATOR
    | NOT_EQUAL_OPERATOR
    ;

atom
   : constant
   | variable
   ;

constant
    : INT_NUMBER
    | DECIMAL_NUMBER
    | SINGLE_QUOTED_TEXT
    | DOUBLE_QUOTED_TEXT
    ;

variable
    : identifier
    ;

identifier
    : IDENTIFIER
    ;

WhiteSpace
   : [ \r\n\t]+ -> skip;

// lexer

fragment DIGIT:    [0-9];
fragment DIGITS:   DIGIT+;
fragment HEXDIGIT: [0-9a-fA-F];

// Only lower case 'x' and 'b' count for hex + bin numbers. Otherwise it's an identifier.
HEX_NUMBER: ('0x' HEXDIGIT+) | ('x\'' HEXDIGIT+ '\'');
BIN_NUMBER: ('0b' [01]+) | ('b\'' [01]+ '\'');

INT_NUMBER: DIGITS;
DECIMAL_NUMBER: DIGITS? DOT_SYMBOL DIGITS;
FLOAT_NUMBER:   (DIGITS? DOT_SYMBOL)? DIGITS [eE] (MINUS_OPERATOR | PLUS_OPERATOR)? DIGITS;

// Operators
EQUAL_OPERATOR:            '=';
EQUAL2_OPERATOR:           '==' -> type(EQUAL_OPERATOR);
GREATER_OR_EQUAL_OPERATOR: '>=';
GREATER_THAN_OPERATOR:     '>';
LESS_OR_EQUAL_OPERATOR:    '<=';
LESS_THAN_OPERATOR:        '<';
NOT_EQUAL_OPERATOR:        '!=';
NOT_EQUAL2_OPERATOR:       '<>' -> type(NOT_EQUAL_OPERATOR);

PLUS_OPERATOR:  '+';
MINUS_OPERATOR: '-';
MULT_OPERATOR:  '*';
DIV_OPERATOR:   '/';

MOD_OPERATOR: '%';

LOGICAL_NOT_OPERATOR: '!';
BITWISE_NOT_OPERATOR: '~';

SHIFT_LEFT_OPERATOR:  '<<';
SHIFT_RIGHT_OPERATOR: '>>';

LOGICAL_AND_OPERATOR: '&&';
BITWISE_AND_OPERATOR: '&';

BITWISE_XOR_OPERATOR: '^';

LOGICAL_OR_OPERATOR:
    '||'
;
BITWISE_OR_OPERATOR: '|';

DOT_SYMBOL:         '.';
COMMA_SYMBOL:       ',';
SEMICOLON_SYMBOL:   ';';
COLON_SYMBOL:       ':';
OPEN_PAR_SYMBOL:    '(';
CLOSE_PAR_SYMBOL:   ')';
OPEN_CURLY_SYMBOL:  '{';
CLOSE_CURLY_SYMBOL: '}';
UNDERLINE_SYMBOL:   '_';

IDENTIFIER:
    [a-zA-Z0-9_]+
    ;

fragment SINGLE_QUOTE: '\'';
fragment DOUBLE_QUOTE: '"';

DOUBLE_QUOTED_TEXT: (
        DOUBLE_QUOTE (('\\' .)? .)*? DOUBLE_QUOTE
    )+
;

SINGLE_QUOTED_TEXT: (
        SINGLE_QUOTE (('\\')? .)*? SINGLE_QUOTE
    )+
;
