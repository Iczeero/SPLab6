package npi.sp.lab6;

import androidx.appcompat.app.AppCompatActivity;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class RegistrationActivity extends AppCompatActivity {

    private EditText nameEditText;
    private EditText studentIdEditText;
    private Button registerButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registration);

        nameEditText = findViewById(R.id.nameEditText);
        studentIdEditText = findViewById(R.id.studentIdEditText);
        registerButton = findViewById(R.id.registerButton);

        registerButton.setOnClickListener(v -> {
            String name = nameEditText.getText().toString();
            String studentId = studentIdEditText.getText().toString();

            if (name.isEmpty() || studentId.isEmpty()) {
                Toast.makeText(RegistrationActivity.this, "Пожалуйста, заполните все поля", Toast.LENGTH_SHORT).show();
            } else {

                SharedPreferences sharedPreferences = getSharedPreferences("StudentData", MODE_PRIVATE);
                SharedPreferences.Editor editor = sharedPreferences.edit();
                editor.putString("Name", name);
                editor.putString("StudentId", studentId);
                editor.apply();

                Toast.makeText(RegistrationActivity.this, "Регистрация успешна", Toast.LENGTH_SHORT).show();
                finish();
            }
        });
    }
}