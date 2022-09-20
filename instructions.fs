\ instructions with 0 operands
s" oooo oooo oooo oooo" @layout
\ =============================

s" No Operation"
  0
  instruction: nop,

s" Sleep"
  n( % 1001 0101 1000 1000 )
  instruction: sleep,

s" Watchdog Reset"
  n( % 1001 0101 1010 1000 )
  instruction: wdr,


s" Clear Carry Flag"
  $9488
  instruction: clc,

s" Set Carry Flag"
  n( % 1001 0100 0000 1000 )
  instruction: sec,

s" Clear Half Carry Flag"
  n( % 1001 0100 1101 1000 )
  instruction: clh,

s" Set Half Carry Flag"
  n( % 1001 0100 0101 1000 )
  instruction: seh,

s" Clear Global Interrupt Enable bit"
  $94F8
  instruction: cli,

s" Set Global Interrupt Enable Bit"
  n( % 1001 0100 0111 1000 )
  instruction: sei,

s" Clear Negative Flag"
  n( % 1001 0100 1010 1000 )
  instruction: cln,

s" Set Negative Flag"
  n( % 1001 0100 0010 1000 )
  instruction: sen,

s" Clear Zero Flag"
  n( % 1001 0100 1001 1000 )
  instruction: clz,

s" Set Zero Flag"
  n( % 1001 0100 0001 1000 )
  instruction: sez,

s" Clear Sign Flag"
  n( % 1001 0100 1100 1000 )
  instruction: cls,

s" Set Sign Flag"
  n( % 1001 0100 0100 1000 )
  instruction: ses,

s" Clear Overflow Flag"
  n( % 1001 0100 1011 1000 )
  instruction: clv,

s" Set Overflow Flag"
  n( % 1001 0100 0011 1000 )
  instruction: sev,

s" Clear T Bit"
  n( % 1001 0100 1110 1000 )
  instruction: clt,

s" Set T Bit"
  n( % 1001 0100 0110 1000 )
  instruction: set,


s" Indirect Jump"
  n( % 1001 0100 0000 1001 )
  instruction: ijmp,

s" Extended Indirect Jump"
  n( % 1001 0100 0001 1001 )
  instruction: eijmp,

s" Indirect Call to Subroutine"
  n( % 1001 0101 0000 1001 )
  instruction: icall,

s" Extended Indirect Call to Subroutine"
  n( % 1001 0101 0001 1001 )
  instruction: iecall,

s" Return from subroutine"
  n( % 1001 0101 0000 1000 )
  instruction: ret,

s" Return from Interrupt"
  n( % 1001 0101 0001 1000 )
  instruction: reti,

\ TODO lpm has 2 other variants, this one loads the value into R0
s" Load Program Memory"
  n( % 1001 0101 1100 1000 )
  instruction: lpm,

\ TODO elpm has 2 other variants, this one loads the value into R0
s" Extended Load Program Memory"
  n( % 1001 0101 1101 1000 )
  instruction: elpm>r0,


(
all the instructions without
operands:
ijmp [x] eijmp [x]
icall [x] eicall [x]
ret [x] reti [x]
lpm [x] elmp [x]
sec [x] clc [x]
sen [x] cln [x]
sez [x] clz [x]
sei [x] cli [x]
ses [x] cls [x]
sev [x] clv [x]
set [x] clt [x]
seh [x] clh [x]
nop [x]
sleep [x]
wdr [x]
)


s" oooo oord dddd rrrr" @layout
\ =============================

s" Compare with Carry"
  n( % 0000 01 )
  instruction: cpc,

s" Subtract with Carry"
  n( % 0000 10 )
  instruction: sbc,

s" Add without Carry"
  n( % 0000 11 )
  instruction: add,

s" Compare Skip if Equal"
  n( % 0001 00 )
  instruction: cpse,

s" Compare"
  n( % 0001 01 )
  instruction: cp,

s" Subtract without Carry"
  n( % 0001 10 )
  instruction: sub,

s" Add with Carry"
  n( % 0001 11 )
  instruction: adc,


s" Logical AND"
  n( % 0010 00 )
  instruction: and,

s" Exclusive OR"
  n( % 0010 01 )
  instruction: eor,

s" Logical Or"
  n( % 0010 10 )
  instruction: or,

s" Copy Register"
  n( % 0010 11 )
  instruction: mov,

s" Multiply Unsigned"
  n( % 1001 11 )
  instruction: mul,


s" oooo oooo kkdd kkkk" @layout
\ =============================

s" Add Immediate to Word"
  n( % 1001 0110 )
  instruction: adiw,
\ cbr is a subset of adiw

s" oooo kkkk dddd kkkk" @layout
\ =============================

s" Compare with Immediate"
  n( % 0011 )
  instruction: cpi,

s" Logical AND with Immediate"
  n( % 0111 )
  instruction: andi,

s" Load Immediate"
  n( % 1110 )
  instruction: ldi,

s" oooo okkk dddd kkkk" @layout
\ =============================

s" Load Direct from Data Space"
  n( % 1010 0 )
  instruction: lds,

s" ooqo qqod dddd oqqq" @layout
\ =============================

s" Load Indirect from Data Space to Register using Y with displacement"
  n( % 10 0 0 1 )
  instruction: ldd,

s" oooo oood dddd oooo" @layout
\  oooo ooor rrrr oooo
\ =============================

s" Load Indirect from Data Space to Register using Y"
  n( % 1000 000 1000 )
  instruction: ldy,

s" Load Indirect from Data Space to Register using Y Post incremented"
  n( % 1001 000 1001 )
  instruction: ldy+,

s" Load Indirect from Data Space to Register using Y Pre decremented"
  n( % 1001 000 1010 )
  instruction: ld-y,

s" Load Indirect from Data Space to Register using X"
  n( % 1001 000 1100 )
  instruction: ldx,

s" Load Indirect from Data Space to Register using X Post incremented"
  n( % 1001 000 1101 )
  instruction: ldx+,

s" Load Indirect from Data Space to Register using X Pre decremented"
  n( % 1001 000 1110 )
  instruction: ld-x,

s" Load and Clear"
  n( % 1001 001 0110 )
  instruction: lac,

s" Load and Set"
  n( % 1001 001 0101 )
  instruction: las,

s" Load and Toggle"
  n( % 1001 001 0111 )
  instruction: lat,

s" One's complement"
  n( % 1001 010 0000 )
  instruction: com,

s" Increment"
  n( % 1001 010 0011 )
  instruction: inc,

s" Arithmetic Shift Right"
  n( % 1001 010 0101 )
  instruction: asr,

s" Decrement"
  n( % 1001 010 1010 )
  instruction: dec,


s" oooo oooo osss oooo" @layout
\ =============================

s" Bit Clear in SREG"
  n( % 1001 0100 1 1000 )
  instruction: bclr,

s" Bit Set in SREG"
  n( % 1001 0100 0 1000 )
  instruction: bset,


s" oooo oood dddd obbb" @layout
\ =============================

s" Bit Load from the T Bit in SREG to a Bit in Register"
  n( % 1111 100 0 )
  instruction: bld,

s" Bit Store from Bit in Register to T Bit in SREG"
  n( % 1111 101 0 )
  instruction: bst,


s" oooo ookk kkkk ksss" @layout
\ =============================

s" Branch if Bit in SREG is Cleared"
  n( % 1111 01 )
  instruction: brbc,

s" Branch if Bit in SREG is Set"
  n( % 1111 00 )
  instruction: brbs,

(

\ The following instructions are
\ equivalent to brbc or brbs with
\ specific values of s

brbc
====
1111 01 000 brcc brsh
1111 01 001 brne
1111 01 011 brvs
1111 01 010 brpl
1111 01 100 brge
1111 01 101 brhc
1111 01 110 brtc
1111 01 111 brid

brbs
====
1111 00 000 brcs brlo
1111 00 001 breq
1111 00 010 brmi
1111 00 011 brvc
1111 00 100 brlt
1111 00 101 brhs
1111 00 110 brts
1111 00 111 brie
)

s" oooo oooo AAAA Abbb" @layout
\ =============================

s" Clear Bit in I/O register"
  n( % 1001 1000 )
  instruction: cbi,

s" oooo oAAd dddd AAAA" @layout
\ =============================

s" Load an I/O Location to Register"
  n( % 1011 0 )
  instruction: in,

(

\ Some instructions have variants:
- lmp has 3 variant
- elpm has 3 variants
- ld has 3 + 4 variants

\ The instruction CLR is EOR Rd, Rd

\ The instruction CBR is ANDI Rd, 0xFF - K

\ There's also an instruction called "DEC" that is used for encryption
\ but I won't implement it unless I need it.

\ Same with FMUL, FMULS and FMULSU

\ 32bits instructions!!!
\ I will probably not implement them, until I need them
oooo oook kkkk oook
kkkkk kkkk kkkk kkkkk
1001 010 111 call
1001 010 110 jmp

oooo oood dddd oooo
kkkkk kkkk kkkk kkkkk
1001 000 0000 lds
1001 001 0000 sts

)