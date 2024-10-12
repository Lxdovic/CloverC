# Clover-C

<img src="https://github.com/user-attachments/assets/c2b630b4-3086-4132-b061-00773c4e7f0a" style="display: block; margin: 0 auto" width="200" alt="cloverC"/>

Built by following the [Writing a C Compiler](https://nostarch.com/writing-c-compiler) book by Nora Sandler

## How to use

```sh
CloverC/Driver > dotnet run [path/to/file.c] [options]
```

### options
```
--lex // lex only
--parse // lex and parse
--codegen // lex, parse and generate code
```