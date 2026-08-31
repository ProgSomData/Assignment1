//1.4
import java.util.HashMap;

abstract class Expr {
    abstract public int eval(HashMap<String,Integer> env);
    abstract public Expr simplify();

}


class CstI extends Expr {
    protected final int i;
     
    public CstI (int i) {
        this.i = i;
    }

    public String toString () {
        return Integer.toString(i);
    }

    public int eval(HashMap<String,Integer> env) {
        return i;
    }

    public Expr simplify() {
        return this;
    }

}

class Var extends Expr {
    protected final String name;

    public Var (String s) {
        this.name = s;
    }

    public String toString () {
        return name;
    }

    public int eval (HashMap<String, Integer> env) {
        return env.get(name);
    }

    public Expr simplify() {
        return this;
    }
}

abstract class Binop extends Expr {
    protected final Expr e1, e2;

    public Binop(Expr e1, Expr e2) {
        this.e1 = e1;
        this.e2 = e2;
    }
}

class Add extends Binop {

    public Add (Expr e1, Expr e2) {
        super(e1, e2);
    }

    public String toString () {
        return "(" + e1.toString() + " + " +  e2.toString() + ")";
    }

    public int eval(HashMap<String,Integer> env) {
        return e1.eval(env) + e2.eval(env);
    }

    public Expr simplify() {
        if (e1 instanceof CstI c && c.i == 0) {return e2;}
        else if (e2 instanceof CstI c && c.i == 0) {return e1;}
        else return this;
    }
}

class Mul extends Binop {
    
    public Mul (Expr e1, Expr e2) {
        super(e1, e2);
    }

    public String toString () {
        return "(" + e1.toString() + " * " +  e2.toString() + ")";
    }

    public int eval(HashMap<String,Integer> env) {
        return e1.eval(env) * e2.eval(env);
    }

        public Expr simplify() {
        if ((e1 instanceof CstI c && c.i == 0) || (e2 instanceof CstI d && d.i == 0)) {return new CstI (0);}
        else if (e2 instanceof CstI c && c.i == 1) {return e1;}
        else if (e1 instanceof CstI c && c.i == 1) {return e2;}
        else return this;
    }
}

class Sub extends Binop {

    public Sub (Expr e1, Expr e2) {
        super(e1, e2);
    }

    public String toString () {
        return "(" + e1.toString() + " - " + e2.toString() + ")"; 
    }

    public int eval(HashMap<String,Integer> env) {
        return e1.eval(env) - e2.eval(env);
    }

    public Expr simplify() {
        if (e1 == e2) return new CstI (0);
        else if (e2 instanceof CstI c && c.i == 0) return e1; 
        else return this;
    }

}


//1.4 (ii)
class exercise14 {
    public static void main (String[] args) {

        Expr e1 = new Add(new CstI (17), new Var("z"));
        System.out.println(e1.toString());

        // 1.4(iii)

        Expr a = new Var ("a");
        Expr e2 = new Mul(new Mul (a, new CstI (4)), a);
        System.out.println(e2);

        Expr e3 = new Sub(
                    new Sub(
                        new CstI (15), new Sub(
                            new CstI (25), new CstI (12))), new CstI (13));
        System.out.println(e3);
        
        // 1.4(iv)
        Expr e4 = new Add(new Var ("b"), new CstI (15));
        System.out.println(e4);
        
    }
}