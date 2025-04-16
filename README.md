# QuantLangCompiler

### This is a compiler for Quant Programming Language


## Syntax
Syntax of QuantLang is designed to be simple and understandable

### Statements
Statement in QuantLang must be ended with ';' token. 
Here is a list of simple statements:

```quantl
// expressions statements:
2 + 5;
someFunc(3) + 65;
// statements:
a = 2 + 2; // assigment statement
let a: i32 = 2 // decl statement
asm("your asm code here"); // asm statement
```
### Types
QuantLang supports:
- `i32` - 32-bit signed integer
- `i16` - 16-bit signed integer
- `i64` - 64-bit signed integer
- `char` - 8-bit signed integer, also a character
- `<type>*` - represents a pointer
- `struct <structname>` - object of custom type
- `<type>[<count>]` - creates static array for \<count\> elements of \<type\>

### Variables
To declarate variable in QuantLang, use:
```quantl
fn main() {
	let <name>: <type>; 	// without initial value
	let <name>: <type> = ...; // with initial value
```

### Functions
To declarate function in QuantLang use
```quantl
fn <funcName>(<args>) -> <returnType> {
	// body
}
```
Example of factorial in QuantLang
```quantl
fn fact(n: i32) -> i32 {
	if n == 0 {
		return 1;
	} else {
		return n * fact(n - 1);
	}
}
```
