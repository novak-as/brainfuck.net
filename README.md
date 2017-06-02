[![Build Status](https://travis-ci.org/novak-as/brainfuck.net.svg?branch=master)](https://travis-ci.org/novak-as/brainfuck.net)

# brainfuck.net

### Usage:

```
compiler -f filename [options]

Options:
  -f, --file=file            file with brainfuck sources
      --assembly_name=VALUE  assembly name
  -v, --version=VALUE        version
  -m, --memory=VALUE         available memory
  -n, --nested=VALUE         max depth of nested loop
  -h, --help                 show this message
```

### Example:

`compiler -f test.bf -m 50`

###### Input:

`++++++++++[>+++++++>++++++++++>+++<<<-]>++.>+.+++++++..+++.>++.<<+++++++++++++++.>.+++.------.--------.>+.`

###### Compiled program output: 
`Hello World!`
