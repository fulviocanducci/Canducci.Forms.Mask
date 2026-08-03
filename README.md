# Canducci.Forms.Mask

Biblioteca mínima que fornece apenas `MaskCurrency` — um adaptador para aplicar máscara de moeda em um `TextBox` do Windows Forms.

O objetivo é ser pequeno, testável e seguro para uso em aplicações WinForms.

## Principais APIs

- MaskCurrency (classe)
- MaskCurrency (extensão para TextBox)

## Funcionalidades

- Formatação de moeda por cultura (padrão `pt-BR`).
- Eventos públicos para observar ou cancelar comportamento interno:
  - `KeyPressCalled` (KeyPressEventHandler) — pode cancelar definindo `e.Handled = true`.
  - `TextChangedCalled` (EventHandler) — notificado antes do parse/format interno.
  - `LeaveCalled` (EventHandler) — notificado após formatação quando o TextBox perde foco.
  - `ValueChanged` (EventHandler) — quando o valor decimal muda.
- `Detach()` / `Dispose()` para remover handlers e evitar leaks.

## Exemplos

Anexar a máscara ao `TextBox`:

```csharp
// attach mask using extension
var mask = myTextBox.MaskCurrency(0m, new CultureInfo("pt-BR"));
```

Cancelar o comportamento interno no KeyPress (exemplo):

```csharp
mask.KeyPressCalled += (s, e) => {
    if (e.KeyChar == '9')
    {
        e.Handled = true; // impede o processamento interno para essa tecla
    }
};
```

Observar TextChanged:

```csharp
mask.TextChangedCalled += (s, e) => {
    var raw = myTextBox.Text; // texto antes do parse interno
};
```

Ação ao perder foco:

```csharp
mask.LeaveCalled += (s, e) => {
    // ação após a máscara formatar o texto
};
```

Liberar recursos:

```csharp
mask.Detach();
mask.Dispose();
```

## Notas

- A lógica de formatação está encapsulada em `CurrencyFormatter` para facilitar testes.
- Esta biblioteca contém apenas `MaskCurrency` por design.

Contribuições e issues são bem-vindas.